using DeliveryProject.DTOs.VehicleDTOs;

namespace DeliveryProject.DTOs.DriverDTOs
{
    public class DriverLoginResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public DisplayVehicleDTO Vehicle { get; set; }
        public string Token { get; set; }

    }
}
