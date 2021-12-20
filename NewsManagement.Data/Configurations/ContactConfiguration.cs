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
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Company).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Leader).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Position).IsRequired().HasMaxLength(50);

            builder.Property(x => x.License).IsRequired().HasMaxLength(int.MaxValue);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Hotline).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Contact_Advertise).IsRequired().HasMaxLength(int.MaxValue);
        }
    }
}
