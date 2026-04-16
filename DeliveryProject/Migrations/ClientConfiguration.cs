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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.ClientID);


            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(70);


            builder.HasOne(c => c.user)
                .WithOne(u => u.client)
                .HasForeignKey<Client>(c => c.UserID);
        }
    }
}
