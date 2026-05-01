using DeliveryProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryProject.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Message).IsRequired().HasMaxLength(500);
                builder.Property(x => x.IsRead).HasDefaultValue(false);
                builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
