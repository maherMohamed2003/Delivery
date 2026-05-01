using DeliveryProject.DTOs.VehicleDTOs;
using DeliveryProject.Models;

namespace DeliveryProject.DTOs.DriverDTOs
{
    public class DisplayDriverDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool isBlocked { get; set; }
        public string RoleName { get; set; }
        public DisplayVehicleDTO Vehicle { get; set; }
    }
}
