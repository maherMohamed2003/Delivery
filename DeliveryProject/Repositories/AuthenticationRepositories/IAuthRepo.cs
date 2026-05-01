namespace DeliveryProject.Repositories.AuthenticationRepositories
{
    public interface IAuthRepo
    {
        public Task<string> GenerateTokenAsync(int userId, string role);

        public Task<string> SendEmailAsync(string Email, string Body , string subject);
        public Task<string> SendNotificationAsync(string title, string Message, int driverId);
    }
}
