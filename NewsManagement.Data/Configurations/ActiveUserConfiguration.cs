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
    public class ActiveUserConfiguration : IEntityTypeConfiguration<ActiveUser>
    {
        public void Configure(EntityTypeBuilder<ActiveUser> builder)
        {
            builder.ToTable("ActiveUser");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.User).WithMany(x => x.ActiveUsers).HasForeignKey(x => x.UserId);
        }
    }
}
