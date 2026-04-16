using DeliveryProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryProject.Migrations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(s => s.ShipmentId);

            builder.Property(s => s.ReceiverName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ReceiverAddress)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ReceiverPhone)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(s => s.client)
                   .WithMany(c => c.shipments)
                   .HasForeignKey(s => s.ClientID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.driver)
                   .WithMany(d => d.shipments)
                   .HasForeignKey(s => s.DriverID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
