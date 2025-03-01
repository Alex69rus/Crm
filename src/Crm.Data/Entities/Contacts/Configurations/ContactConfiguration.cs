using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Data.Entities.Contacts.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.MiddleName)
            .HasMaxLength(100);

        builder.Property(x => x.NamePrefix)
            .HasMaxLength(20);

        builder.Property(x => x.Gender)
            .HasMaxLength(50);

        builder.Property(x => x.TaxId)
            .HasMaxLength(50);

        builder.Property(x => x.Citizenship)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // One-to-Many relationships
        builder.HasMany(x => x.Addresses)
            .WithOne(x => x.Contact)
            .HasForeignKey(x => x.ContactId)
            .IsRequired();

        builder.HasMany(x => x.Phones)
            .WithOne(x => x.Contact)
            .HasForeignKey(x => x.ContactId)
            .IsRequired();
    }
} 