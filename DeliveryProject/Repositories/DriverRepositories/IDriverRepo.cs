using DeliveryProject.DTOs.DriverDTOs;
using DeliveryProject.DTOs.NotificationDTOs;
using DeliveryProject.DTOs.ShipmentsDTOs;

namespace DeliveryProject.Repositories.DriverRepositories
{
    public interface IDriverRepo
    {
        public Task<DisplayDriverDTO> RegisterAsDriver(DriverRegisterDTO driverRegisterDTO);
        public Task<DriverLoginResponseDTO> Login(DriverLoginDTO login);
        public Task<DisplayShipmentDetails> AssignShipmentAsync(AssignShipmentDTO dto);
        public Task<DisplayShipmentDetails> MarkShipmentAsDeliveredAsync(int shipmentId);
        public Task<List<DisplayDriverDTO>> GetAllDriversAsync();
        public Task<bool> BlockDriver(int id);
        public Task<bool> UnBlockDriver(int id);
        public Task<List<DisplayNotificationDTO>> DisplayNotificationsPerDriverAsync(int id);
        public Task<DisplayDriverDTO> GetDriverByIdAsync(int id);
        public Task<DisplayDriverDTO> UpdateDriverAsync(DriverUpdateDTO driverUpdateDTO);
        public Task<bool> DeleteDriverAsync(int id);
        public Task<DriversOverviewDTO> DisplayDriversOverviewAsync();
        public Task<DisplayNotificationDTO> SendNotificationAsync(SendNotificationDTO dto);
        public Task<GetOneDriverOverviewDTO> GetOneDriverOverviewAsync(int driverId);
        public Task<List<DisplayShipmentDetails>> GetDriverShipmentsAsync(int driverId);

    }
}
