using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Data.Entities.Contacts.Configurations;

public class ContactAddressConfiguration : IEntityTypeConfiguration<ContactAddress>
{
    public void Configure(EntityTypeBuilder<ContactAddress> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Street)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.State)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PostalCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ContactId)
            .IsRequired();
    }
} 