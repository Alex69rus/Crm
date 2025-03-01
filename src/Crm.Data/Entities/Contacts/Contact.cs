using Crm.Data.Entities.Accounts;

namespace Crm.Data.Entities.Contacts;

public class Contact
{
    public string Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public string? NamePrefix { get; private set; }
    public string? Gender { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }
    public DateOnly? DateOfDeath { get; private set; }
    public string? TaxId { get; private set; }
    public string? Citizenship { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public ICollection<ContactAddress> Addresses { get; private set; } = new List<ContactAddress>();
    public ICollection<ContactPhone> Phones { get; private set; } = new List<ContactPhone>();
    public ICollection<AccountOwner> AccountOwners { get; private set; } = new List<AccountOwner>();

    public Contact(
        string id,
        string firstName,
        string lastName,
        string? namePrefix = null,
        string? middleName = null,
        string? gender = null,
        DateOnly? dateOfBirth = null,
        DateOnly? dateOfDeath = null,
        string? taxId = null,
        string? citizenship = null)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        NamePrefix = namePrefix;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
        TaxId = taxId;
        Citizenship = citizenship;
    }

    public void UpdateNames(
        string firstName, 
        string lastName,
        string? namePrefix = null,
        string? middleName = null)
    {
        FirstName = firstName;
        LastName = lastName;
        NamePrefix = namePrefix;
        MiddleName = middleName;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePersonalInfo(
        string? gender,
        DateOnly? dateOfBirth,
        DateOnly? dateOfDeath,
        string? taxId,
        string? citizenship)
    {
        Gender = gender;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
        TaxId = taxId;
        Citizenship = citizenship;
        UpdatedAt = DateTime.UtcNow;
    }
} 