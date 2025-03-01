using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Data.Entities.Accounts.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.AccountNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ShortName)
            .HasMaxLength(50);

        builder.Property(x => x.RegistrationType)
            .HasMaxLength(50);

        builder.Property(x => x.SponsorCompany)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasMaxLength(50);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Discretion)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // One-to-One relationship with AccountBalance
        builder.HasOne(x => x.Balance)
            .WithOne(x => x.Account)
            .HasForeignKey<AccountBalance>(x => x.AccountId)
            .IsRequired();

        // One-to-Many relationship with AccountOwner
        builder.HasMany(x => x.AccountOwners)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired();
    }
} 