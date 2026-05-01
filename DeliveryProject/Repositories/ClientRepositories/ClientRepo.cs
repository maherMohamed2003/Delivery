using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryProject.Data;
using DeliveryProject.DTOs.ClientDTOs;
using DeliveryProject.Models;
using DeliveryProject.Repositories.AuthenticationRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliveryProject.Repositories.ClientRepositories
{
    public class ClientRepo : IClientRepo
    {
        private readonly AppDbContext _context;
        private readonly IAuthRepo _auth;
        public ClientRepo(AppDbContext context, IAuthRepo auth)
        {
            _context = context;
            _auth = auth;
        }

        public async Task<DisplayClientDetailsDTO> AddNewClientAsync(AddClientDTO DTO)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Email == DTO.Email || x.CompanyName == DTO.CompanyName);
            if (client != null)
                return null;
            var role = new Role
            {
                RoleName = "Company"
            };

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            var hasher = new PasswordHasher<Client>();
            var newClient = new Client
            {
                Email = DTO.Email,
                CompanyName = DTO.CompanyName,
                Phone = DTO.Phone,
                RoleID = role.RoleID,
                isBlocked = false
            };
            
            var hashedPassword = hasher.HashPassword(newClient, DTO.Password);
            newClient.Password = hashedPassword;
            await _context.Clients.AddAsync(newClient);
            await _context.SaveChangesAsync();

            return new DisplayClientDetailsDTO
            {
                ID = newClient.Id,
                Email = newClient.Email,
                CompanyName = newClient.CompanyName,
                Phone = newClient.Phone,
                Role = role.RoleName,
                IsBlocked = newClient.isBlocked
            };
        }

        public async Task<bool> BlockedClientAsync(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                return false;
            }
            client.isBlocked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClientAsync(int Id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == Id);
            if (client == null)
            {
                return false;
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DisplayClientDetailsDTO>> GetAllClientsAsync()
        {
            var clients = await _context.Clients.Select(x => new DisplayClientDetailsDTO
            {
                CompanyName = x.CompanyName,
                Phone = x.Phone,
                Email = x.Email,
                Role = x.Role.RoleName,
                ID = x.Id,
                IsBlocked = x.isBlocked
            }).ToListAsync();
            return clients;
        }

        public async Task<DisplayClientDetailsDTO> GetClientByIdAsync(int Id)
        {
            var client = await _context.Clients.Where(x => x.Id == Id).Select(x => new DisplayClientDetailsDTO
            {
                CompanyName = x.CompanyName,
                Phone = x.Phone,
                Email = x.Email,
                Role = x.Role.RoleName,
                ID = Id,
                IsBlocked = x.isBlocked
            }).FirstOrDefaultAsync();
            if (client == null)
                return null;
            return client;

        }

        public async Task<ClientLoginResponseDTO> LoginAsClientAsync(ClientLoginDTO DTO)
        {
            var client = await _context.Clients.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == DTO.Email);
            
            if (client == null || client.isBlocked)
                return null;  

            var hasher = new PasswordHasher<Client>();
            var result = hasher.VerifyHashedPassword(client, client.Password, DTO.Password);
            if(result == PasswordVerificationResult.Failed)
                return null;
            
            var token = await _auth.GenerateTokenAsync(client.Id,client.Role.RoleName);

            return new ClientLoginResponseDTO
            {
                ID = client.Id,
                Email = client.Email,
                CompanyName = client.CompanyName,
                Phone = client.Phone,
                Role = client.Role.RoleName,
                Token = token
            };
        }

        public async Task<bool> UnBlockedClientAsync(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                return false;
            }
            client.isBlocked = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DisplayClientDetailsDTO> UpdateClientDataAsync(UpdateClientDTO DTO)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == DTO.Id);
            if (client == null)
            {
                return null;
            }
            client.CompanyName = DTO.CompanyName;
            client.Email = DTO.Email;
            client.Phone = DTO.Phone;
            if (!string.IsNullOrEmpty(DTO.Password))
            {
                var hasher = new PasswordHasher<Client>();
                var hashedPassword = hasher.HashPassword(client, DTO.Password);
                client.Password = hashedPassword;
            }
            await _context.SaveChangesAsync();
             var result = await _context.Clients.Where(x => x.Id == client.Id).Select(x => new DisplayClientDetailsDTO
            {
                ID = x.Id,
                CompanyName = x.CompanyName,
                Email = x.Email,
                Phone = x.Phone,
                Role = x.Role.RoleName,
                IsBlocked = x.isBlocked
            }).FirstOrDefaultAsync();
            return result;
        }
    }
}
