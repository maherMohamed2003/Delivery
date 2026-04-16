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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentID);


            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.shipment)
                   .WithOne(s => s.payment)
                   .HasForeignKey<Payment>(p => p.ShipmentID);

            builder.HasOne(p => p.client)
                   .WithMany(c => c.payments)
                   .HasForeignKey(p => p.ClientID);
        }
    }
}
