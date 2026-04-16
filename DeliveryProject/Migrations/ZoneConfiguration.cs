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
    public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasKey(z => z.ZoneID);

            builder.Property(z => z.ZoneName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(z => z.City)
                   .IsRequired()
                   .HasMaxLength(20);

       
            builder.HasMany(z => z.drivers)
                   .WithOne(d => d.zone)
                   .HasForeignKey(d => d.ZoneID) 
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
