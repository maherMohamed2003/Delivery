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
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.DriverID);

            builder.Property(d => d.LicenseNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(d => d.status)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(d => d.user)
                   .WithOne(u => u.driver)
                   .HasForeignKey<Driver>(d => d.UserID);

           
            builder.HasOne(d => d.vehicle)
                   .WithMany(v => v.drivers)
                   .HasForeignKey(d => d.VehicleID);

            
            builder.HasOne(d => d.zone)
                   .WithMany(z => z.drivers)
                   .HasForeignKey(d => d.ZoneID);

        }
    }
}
