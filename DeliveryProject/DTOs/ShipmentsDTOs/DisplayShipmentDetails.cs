using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.DTOs.ShipmentsDTOs
{
    public class DisplayShipmentDetails
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhone { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string ClientName { get; set; }
        public string? DriverName { get; set; }
        public decimal EGPAmount { get; set; }
        public string NowStatus { get; set; }
    }
}
