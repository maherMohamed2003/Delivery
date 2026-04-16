using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string CompanyName { get; set; }

        public int UserID { get; set; }
        public User user { get; set; }

        public ICollection<Shipment> shipments { get; set; }
        public ICollection<Payment> payments { get; set; }
    }
}
