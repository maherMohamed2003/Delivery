using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using DeliveryProject.Authentication;
using DeliveryProject.Data;
using DeliveryProject.Models;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryProject.Repositories.AuthenticationRepositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly JWTOptions _jwt;
        private readonly AppDbContext _context;
        public AuthRepo(JWTOptions jwt, AppDbContext context)
        {
            _jwt = jwt;
            _context = context;
        }

        public async Task<string> GenerateTokenAsync(int userId, string role)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var jwtDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience,
                Expires = DateTime.Now.AddDays(_jwt.DurationInDays),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                })
            };

            var securityToken = jwtHandler.CreateToken(jwtDescriptor);
            var accessToken = jwtHandler.WriteToken(securityToken);
            return accessToken;

        }

        public async Task<string> SendEmailAsync(string Email, string Body , string subject)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("maheralzamzamy3@gmail.com", "sxlv fxga ffqx xinw"),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress("maheralzamzamy3@gmail.com"),
                Subject = subject,
                Body = Body,
                IsBodyHtml = true
            };

            mail.To.Add(Email);
            smtp.SendMailAsync(mail);
            return "Email Sent Successfully";
        }

        public async Task<string> SendNotificationAsync(string title,   string message, int driverId)
        {
            var notification = new Notification
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false,
                DriverID = driverId
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return "Notification Sent Successfully";
        }
    }
}
