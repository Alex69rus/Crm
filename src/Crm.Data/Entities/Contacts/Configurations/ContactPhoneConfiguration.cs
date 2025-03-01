using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Data.Entities.Contacts.Configurations;

public class ContactPhoneConfiguration : IEntityTypeConfiguration<ContactPhone>
{
    public void Configure(EntityTypeBuilder<ContactPhone> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.ContactId)
            .IsRequired();
    }
} 