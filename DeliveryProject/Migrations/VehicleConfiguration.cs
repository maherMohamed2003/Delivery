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
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.VehicleID);

            builder.Property(v => v.VehicleType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(v => v.PlateNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(v => v.PlateNumber)
                   .IsUnique();


            builder.Property(v => v.Status)
                   .HasMaxLength(20);

            
            builder.HasMany(v => v.drivers)
                   .WithOne(d => d.vehicle)
                   .HasForeignKey(d => d.VehicleID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
