namespace Crm.Data.Entities.Contacts;

public class ContactPhone
{
    public string Id { get; private set; }
    public string PhoneNumber { get; private set; }
    public PhoneType Type { get; private set; }
    public bool IsPrimary { get; private set; }

    // Foreign key
    public string ContactId { get; private set; }
    public Contact Contact { get; private set; } = null!;

    public ContactPhone(string id, string contactId, string phoneNumber, PhoneType type)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        Type = type;
        ContactId = contactId;
    }
} 