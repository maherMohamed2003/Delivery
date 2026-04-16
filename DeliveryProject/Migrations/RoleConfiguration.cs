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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey (r => r.RoleID);

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasMany(r => r.Users)
                .WithOne(u => u.UserRoles)
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
