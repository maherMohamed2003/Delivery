using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class Driver 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public string Status { get; set; }
        public bool isBlocked { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<Shipment> Shipments  { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
