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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Company).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Email).IsUnicode(false).IsRequired().HasMaxLength(100);

            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(11);

            builder.Property(x => x.Img).HasMaxLength(255);

            builder.HasOne(x => x.Account).WithOne(x => x.Client).HasForeignKey<Client>(x => x.Id);
        }

    }
}
