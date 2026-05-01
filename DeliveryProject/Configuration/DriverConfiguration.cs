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
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.LicenseNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(d => d.Status)
                .IsRequired()
                .HasMaxLength(20);
           
            builder.HasOne(d => d.Vehicle)
                   .WithMany(v => v.drivers)
                   .HasForeignKey(d => d.VehicleID);

            builder.Property(x => x.Email)
                .HasMaxLength(50).IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(11).IsRequired();




            builder.HasMany(x => x.Notifications)
                .WithOne(n => n.Driver)
                .HasForeignKey(n => n.DriverID)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
