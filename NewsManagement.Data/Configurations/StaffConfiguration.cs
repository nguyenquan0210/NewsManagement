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
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(int.MaxValue);

            builder.Property(x => x.Email).IsRequired().IsUnicode(false).HasMaxLength(100);

            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(11);

            builder.Property(x => x.Img).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.Account).WithOne(x => x.Staff).HasForeignKey<Staff>(x => x.Id);
        }

    }
}
