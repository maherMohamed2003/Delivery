using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryProject.DTOs.ClientDTOs;

namespace DeliveryProject.Repositories.ClientRepositories
{
    public interface IClientRepo
    {
        public Task<DisplayClientDetailsDTO> AddNewClientAsync(AddClientDTO DTO);
        public Task<DisplayClientDetailsDTO> GetClientByIdAsync(int Id);
        public Task<List<DisplayClientDetailsDTO>> GetAllClientsAsync();
        public Task<ClientLoginResponseDTO> LoginAsClientAsync(ClientLoginDTO DTO);
        public Task<bool> BlockedClientAsync(int id);
        public Task<bool> UnBlockedClientAsync(int id);
        public Task<DisplayClientDetailsDTO> UpdateClientDataAsync(UpdateClientDTO DTO);
        public Task<bool> DeleteClientAsync(int Id);
    }
}
