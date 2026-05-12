namespace DeliveryProject.DTOs.ShipmentsDTOs
{
    public class ShipmentOverviewDTO
    {
        public int TotalShipments { get; set; }
        public int PendingShipments { get; set; }
        public int DeliveredShipments { get; set; }
        public int CancelledShipments { get; set; }
        public int AssignedShipments { get; set; }
        public decimal TotalRevinuations { get; set; }
    }
}
