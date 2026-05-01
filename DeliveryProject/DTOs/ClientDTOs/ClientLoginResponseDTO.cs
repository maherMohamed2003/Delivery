namespace DeliveryProject.DTOs.ClientDTOs
{
    public class ClientLoginResponseDTO
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
