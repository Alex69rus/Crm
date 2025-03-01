using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Data.Entities.Accounts.Configurations;

public class AccountBalanceConfiguration : IEntityTypeConfiguration<AccountBalance>
{
    public void Configure(EntityTypeBuilder<AccountBalance> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Balance)
            .IsRequired();

        builder.Property(x => x.LastUpdatedAt)
            .IsRequired();

        builder.Property(x => x.AccountId)
            .IsRequired();
    }
} 