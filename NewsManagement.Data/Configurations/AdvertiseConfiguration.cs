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
    public class AdvertiseConfiguration : IEntityTypeConfiguration<Advertise>
    {
        public void Configure(EntityTypeBuilder<Advertise> builder)
        {
            builder.ToTable("Advertise");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(int.MaxValue);

            builder.Property(x => x.Url).IsRequired().IsUnicode(false).HasMaxLength(int.MaxValue);

            builder.Property(x => x.UrlImg).IsRequired().IsUnicode(false).HasMaxLength(int.MaxValue);

            builder.HasOne(x => x.Order).WithMany(x => x.Advertises).HasForeignKey(x => x.OrderId);
            
        }
    }
}
