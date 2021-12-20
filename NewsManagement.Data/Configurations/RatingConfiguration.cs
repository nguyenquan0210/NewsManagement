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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Rating");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Checkrating).IsRequired().HasMaxLength(50);


            builder.HasOne(x => x.News).WithMany(x => x.Ratings).HasForeignKey(x => x.NewsId);

            builder.HasOne(x => x.User).WithMany(x => x.Ratings).HasForeignKey(x => x.UserId);
        }
    }
}
