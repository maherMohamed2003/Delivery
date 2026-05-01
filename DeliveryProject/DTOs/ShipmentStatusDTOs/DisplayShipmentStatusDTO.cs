using DeliveryProject.DTOs.ShipmentsDTOs;
using DeliveryProject.Models;

namespace DeliveryProject.DTOs.ShipmentStatusDTOs
{
    public class DisplayShipmentStatusDTO
    {
        public int StatusID { get; set; }
        public string StatusValue { get; set; }
        public DateTime ChangeAt { get; set; }
        public DisplayShipmentDetails shipment { get; set; }

    }
}
