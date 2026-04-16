using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Models
{
    public class Zone
    {
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
        public string City { get; set; }

        public ICollection<Driver> drivers { get; set; }
    }
}
