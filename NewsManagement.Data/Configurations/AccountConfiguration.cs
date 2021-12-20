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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);

            builder.HasOne(x => x.AccountType).WithMany(x => x.Accounts).HasForeignKey(x => x.AccountTypeId);
        }
    }
}
