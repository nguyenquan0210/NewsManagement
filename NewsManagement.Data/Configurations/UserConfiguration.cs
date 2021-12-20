using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsManagement.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Address).HasMaxLength(255);

            builder.Property(x => x.PhoneNumber).HasMaxLength(11);

            builder.Property(x => x.Img).HasMaxLength(255);

            builder.HasOne(x => x.Account).WithOne(x => x.Users).HasForeignKey<User>(x => x.Id);
        }
    }
}
