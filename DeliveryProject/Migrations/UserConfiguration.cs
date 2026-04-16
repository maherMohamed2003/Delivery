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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserID);


            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);


            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(20);


            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);


            builder.HasOne(u => u.UserRoles)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);
        }
    }
}
