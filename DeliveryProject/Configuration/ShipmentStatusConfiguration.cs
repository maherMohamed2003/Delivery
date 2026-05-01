using DeliveryProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Configuration
{
    public class ShipmentStatusConfiguration : IEntityTypeConfiguration<ShipmentStatus>
    {
        public void Configure(EntityTypeBuilder<ShipmentStatus> builder)
        {
            builder.HasKey(sh => sh.StatusID);
            builder.Property(x => x.StatusID).UseIdentityColumn(1, 1);

            builder.Property(sh => sh.StatusValue)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(sh => sh.ChangeAt)
                .IsRequired();

            builder.HasOne(sh => sh.shipment)
                .WithMany(s => s.shipmentStatuses)
                .HasForeignKey(sh => sh.ShipmentID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
