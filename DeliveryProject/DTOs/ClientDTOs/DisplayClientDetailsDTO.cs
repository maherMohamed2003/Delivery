using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.DTOs.ClientDTOs
{
    public class DisplayClientDetailsDTO
    {
        public int ID { get; set; }
        public bool IsBlocked { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

    }
}
