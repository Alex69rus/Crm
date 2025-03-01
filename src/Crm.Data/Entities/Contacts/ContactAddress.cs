namespace Crm.Data.Entities.Contacts;

public class ContactAddress
{
    public string Id { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }
    public bool IsPrimary { get; private set; }

    // Foreign key
    public string ContactId { get; private set; }
    public Contact Contact { get; private set; } = null!;

    public ContactAddress(string id, string contactId, string street, string city, string state, string postalCode, string country)
    {
        Id = id;
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
        ContactId = contactId;
    }
} 