using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.DTOs.ShipmentsDTOs
{
    public class MakeShipmentDTO
    {
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public decimal EGPAmount { get; set; }
        public int ClientID { get; set; }

    }
}
