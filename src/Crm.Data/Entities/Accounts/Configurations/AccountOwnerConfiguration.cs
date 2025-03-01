using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Data.Entities.Accounts.Configurations;

public class AccountOwnerConfiguration : IEntityTypeConfiguration<AccountOwner>
{
    public void Configure(EntityTypeBuilder<AccountOwner> builder)
    {
        // Composite key for M:M relationship
        builder.HasKey(x => new { x.AccountId, x.ContactId });

        builder.Property(x => x.AssignedAt)
            .IsRequired();

        // Many-to-One relationships
        builder.HasOne(x => x.Account)
            .WithMany(x => x.AccountOwners)
            .HasForeignKey(x => x.AccountId)
            .IsRequired();

        builder.HasOne(x => x.Contact)
            .WithMany(x => x.AccountOwners)
            .HasForeignKey(x => x.ContactId)
            .IsRequired();
    }
} 