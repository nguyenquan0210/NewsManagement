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
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Content).IsRequired().HasMaxLength(int.MaxValue);

            builder.Property(x => x.Content).IsRequired().IsUnicode(false).HasMaxLength(int.MaxValue);

            builder.Property(x => x.Img).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Keyword).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.AppUser).WithMany(x => x.News).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Eventss).WithMany(x => x.News).HasForeignKey(x => x.EventId);

            builder.HasOne(x => x.City).WithMany(x => x.News).HasForeignKey(x => x.CityId);

            builder.HasOne(x => x.Topic).WithMany(x => x.News).HasForeignKey(x => x.TopicId);
        }
    }
}
