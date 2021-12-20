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
    public class ServiceConfiguration : IEntityTypeConfiguration<Servicess>
    {
        public void Configure(EntityTypeBuilder<Servicess> builder)
        {
            builder.ToTable("Servicess");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Period).IsRequired().HasDefaultValue(1);

            builder.Property(x => x.Price).IsRequired();
        }
    }
}
