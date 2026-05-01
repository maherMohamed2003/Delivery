using System.Net;
using DeliveryProject.Data;
using DeliveryProject.DTOs.DriverDTOs;
using DeliveryProject.DTOs.NotificationDTOs;
using DeliveryProject.DTOs.ShipmentsDTOs;
using DeliveryProject.DTOs.VehicleDTOs;
using DeliveryProject.Models;
using DeliveryProject.Repositories.AuthenticationRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliveryProject.Repositories.DriverRepositories
{
    public class DriverRepo : IDriverRepo
    {
        private readonly AppDbContext _context;
        private readonly IAuthRepo _auth;

        public DriverRepo(AppDbContext context , IAuthRepo auth)
        {
            _context = context;
            _auth = auth;
        }

        public async Task<DisplayShipmentDetails> AssignShipmentAsync(AssignShipmentDTO dto)
        {
            var shipment = await _context.Shipment.FirstOrDefaultAsync(s => s.ShipmentId == dto.ShipmentID);
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == dto.DriverID);
            if (shipment == null || driver == null)
            {
                return null;
            }
            shipment.DriverID = dto.DriverID;
            driver.Status = "On Trip";
            var shipmentStatus = new ShipmentStatus
            {
                StatusValue = "Assigned",
                ShipmentID = shipment.ShipmentId,
                ChangeAt = DateTime.Now
            };
            await _context.ShipmentStatus.AddAsync(shipmentStatus);
            await _context.SaveChangesAsync();
            
            var res = await _context.Shipment.Where(x => x.ShipmentId == shipment.ShipmentId).Select(x => new DisplayShipmentDetails
            {
                Id = shipment.ShipmentId,
                SenderAddress = shipment.SenderAddress,
                SenderName = shipment.SenderName,
                SenderPhone = shipment.SenderPhone,
                ReceiverAddress = shipment.ReceiverAddress,
                ReceiverName = shipment.ReceiverName,
                ReceiverPhone = shipment.ReceiverPhone,
                CreateAt = shipment.CreateAt,
                DeliveredAt = shipment.DeliveredAt,
                ClientName = x.client.CompanyName,
                DriverName = x.driver.Name,
                EGPAmount = shipment.EGPAmount
            }).FirstOrDefaultAsync();
            if(res == null)
                return null;
            return res;
        }

        public async Task<bool> BlockDriver(int id)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            if (driver == null)
            {
                return false;
            }
            driver.isBlocked = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            if (driver == null)
            {
                return false;
            }
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DisplayNotificationDTO>> DisplayNotificationsPerDriverAsync(int id)
        {
            var notifications = await _context.Notifications.Where(x => x.DriverID == id).Select(x => new DisplayNotificationDTO{
                IsRead = x.IsRead,
                Id = x.Id,
                Message = x.Message,
                CreatedAt = x.CreatedAt
            }).ToListAsync();
            return notifications;
        }

        public Task<List<DisplayDriverDTO>> GetAllDriversAsync()
        {
            var drivers = _context.Drivers.Select(d => new DisplayDriverDTO
            {
                Id = d.Id,
                Name = d.Name,
                LicenseNumber = d.LicenseNumber,
                Status = d.Status,
                isBlocked = d.isBlocked,
                Email = d.Email,
                Phone = d.Phone,
                RoleName = d.Role.RoleName,
                Vehicle = new DisplayVehicleDTO
                {
                    VehicleId = d.Vehicle.VehicleID,
                    VehicleType = d.Vehicle.VehicleType,
                    PlateNumber = d.Vehicle.PlateNumber,
                    Status = d.Vehicle.Status
                }
            }).ToListAsync();
            return drivers;
        }

        public async Task<DisplayDriverDTO> GetDriverByIdAsync(int id)
        {
            var driver = await _context.Drivers.Where(d => d.Id == id).Select(d => new DisplayDriverDTO
            {
                Id = d.Id,
                Name = d.Name,
                LicenseNumber = d.LicenseNumber,
                Status = d.Status,
                isBlocked = d.isBlocked,
                Email = d.Email,
                Phone = d.Phone,
                RoleName = d.Role.RoleName,
                Vehicle = new DisplayVehicleDTO
                {
                    VehicleId = d.Vehicle.VehicleID,
                    VehicleType = d.Vehicle.VehicleType,
                    PlateNumber = d.Vehicle.PlateNumber,
                    Status = d.Vehicle.Status
                }
            }).FirstOrDefaultAsync();
            return driver;  
        }

        public async Task<DriverLoginResponseDTO> Login(DriverLoginDTO login)
        {
            var driver = await _context.Drivers.Include(x => x.Vehicle).Where(x => x.Email == login.Email).FirstOrDefaultAsync();
            if (driver == null || driver.isBlocked)
            {
                return null;
            }
            var hasher = new PasswordHasher<Driver>();
            var result = hasher.VerifyHashedPassword(driver, driver.Password, login.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var token = await _auth.GenerateTokenAsync(driver.Id, "Driver");
            return new DriverLoginResponseDTO
            {
                Id = driver.Id,
                Name = driver.Name,
                LicenseNumber = driver.LicenseNumber,
                Status = driver.Status,
                Email = driver.Email,
                Phone = driver.Phone,
                RoleName = driver.Role.RoleName,
                Vehicle = new DisplayVehicleDTO
                {
                    VehicleId = driver.Vehicle.VehicleID,
                    VehicleType = driver.Vehicle.VehicleType,
                    PlateNumber = driver.Vehicle.PlateNumber,
                    Status = driver.Vehicle.Status
                },
                Token = token
            };
        }

        public async Task<DisplayShipmentDetails> MarkShipmentAsDeliveredAsync(int shipmentId)
        {
            var shipment = await _context.Shipment.FirstOrDefaultAsync(s => s.ShipmentId == shipmentId);
            if (shipment == null)
            {
                return null;
            }
            shipment.DeliveredAt = DateTime.Now;
            var shipmentStatus = new ShipmentStatus
            {
                StatusValue = "Delivered",
                ShipmentID = shipment.ShipmentId,
                ChangeAt = DateTime.Now
            };
            await _context.ShipmentStatus.AddAsync(shipmentStatus);
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == shipment.DriverID);
            driver.Status = "Avaliable";
            await _context.SaveChangesAsync();
            var res = await _context.Shipment.Where(x => x.ShipmentId == shipment.ShipmentId).Select(x => new DisplayShipmentDetails
            {
                Id = shipment.ShipmentId,
                ReceiverAddress = shipment.ReceiverAddress,
                ReceiverName = shipment.ReceiverName,
                ReceiverPhone = shipment.ReceiverPhone,
                CreateAt = shipment.CreateAt,
                DeliveredAt = shipment.DeliveredAt,
                ClientName = x.client.CompanyName,
                DriverName = x.driver.Name,
                EGPAmount = shipment.EGPAmount
            }).FirstOrDefaultAsync();
            if (res == null)
                return null;
            return res;
        }

        public async Task<DisplayDriverDTO> RegisterAsDriver(DriverRegisterDTO driverRegisterDTO)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Email == driverRegisterDTO.Email);

            if (driver != null)
            {
                return null;
            }
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.PlateNumber == driverRegisterDTO.PlateNumber);
            if (vehicle == null)
            {

                vehicle = new Vehicle
                {
                    VehicleType = driverRegisterDTO.VehicleType,
                    PlateNumber = driverRegisterDTO.PlateNumber,
                    Status = driverRegisterDTO.VehicleStatus
                };

                await _context.Vehicles.AddAsync(vehicle);
                await _context.SaveChangesAsync();
            }
            var role = new Role
            {
                RoleName = "Driver"
            };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            
            var newDriver = new Driver
            {
                LicenseNumber = driverRegisterDTO.LicenseNumber,
                Status = "Avaliable",
                Email = driverRegisterDTO.Email,
                Phone = driverRegisterDTO.Phone,
                VehicleID = vehicle.VehicleID,
                RoleID = role.RoleID,
                isBlocked = false,
                Name = driverRegisterDTO.Name
            };
            var hasher = new PasswordHasher<Driver>();
            var hashedPassword = hasher.HashPassword(newDriver, driverRegisterDTO.Password);
            newDriver.Password = hashedPassword;
            await _context.Drivers.AddAsync(newDriver);
            await _context.SaveChangesAsync();

            return new DisplayDriverDTO
            {
                Id = newDriver.Id,
                LicenseNumber = newDriver.LicenseNumber,
                Status = newDriver.Status,
                Email = newDriver.Email,
                Phone = newDriver.Phone,
                isBlocked = newDriver.isBlocked,
                RoleName = "Driver",
                Vehicle = new DisplayVehicleDTO
                {
                    VehicleId = vehicle.VehicleID,
                    VehicleType = vehicle.VehicleType,
                    PlateNumber = vehicle.PlateNumber,
                    Status = vehicle.Status
                }
            };

        }

        public async Task<bool> UnBlockDriver(int id)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            if (driver == null)
            {
                return false;
            }
            driver.isBlocked = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DisplayDriverDTO> UpdateDriverAsync(DriverUpdateDTO driverUpdateDTO)
        {
            var driver = await _context.Drivers.Include(x => x.Vehicle).FirstOrDefaultAsync(d => d.Id == int.Parse(driverUpdateDTO.Id));
            if (driver == null)
            {
                return null;
            }

            driver.Name = driverUpdateDTO.Name;
            driver.Email = driverUpdateDTO.Email;
            driver.Phone = driverUpdateDTO.Phone;
            driver.LicenseNumber = driverUpdateDTO.LicenseNumber;
            driver.Vehicle.VehicleType = driverUpdateDTO.VehicleType;
            driver.Vehicle.PlateNumber = driverUpdateDTO.PlateNumber;
            driver.Vehicle.Status = driverUpdateDTO.VehicleStatus;

            if (!string.IsNullOrEmpty(driverUpdateDTO.Password))
            {
                var hasher = new PasswordHasher<Driver>();
                var hashedPassword = hasher.HashPassword(driver, driverUpdateDTO.Password);
                driver.Password = hashedPassword;
            }

            await _context.SaveChangesAsync();

            return new DisplayDriverDTO
            {
                Id = driver.Id,
                Name = driver.Name,
                LicenseNumber = driver.LicenseNumber,
                Status = driver.Status,
                isBlocked = driver.isBlocked,
                Email = driver.Email,
                Phone = driver.Phone,
                RoleName = driver.Role.RoleName,
                Vehicle = new DisplayVehicleDTO
                {
                    VehicleId = driver.Vehicle.VehicleID,
                    VehicleType = driver.Vehicle.VehicleType,
                    PlateNumber = driver.Vehicle.PlateNumber,
                    Status = driver.Vehicle.Status
                }
            };
        }
    }
}
