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
    public class EventConfiguration : IEntityTypeConfiguration<Eventss>
    {
        public void Configure(EntityTypeBuilder<Eventss> builder)
        {
            builder.ToTable("Eventss");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.SortOrder).HasDefaultValue(1);

            builder.Property(x => x.Hot).HasDefaultValue(false);

            builder.HasOne(x => x.Category).WithMany(x => x.Eventsses).HasForeignKey(x => x.CategoryId);
        }
    }
}
