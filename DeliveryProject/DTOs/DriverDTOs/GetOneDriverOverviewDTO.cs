namespace DeliveryProject.DTOs.DriverDTOs
{
    public class GetOneDriverOverviewDTO
    {
        public int TotalShipments { get; set; }
        public int DeliveredShipments { get; set; }
        public int CancelledShipments { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
