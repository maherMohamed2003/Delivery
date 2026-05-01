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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(x => x.Email)
                .HasMaxLength(50).IsRequired();

            builder.Property(x => x.Password).IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(11).IsRequired();
        }
    }
}
