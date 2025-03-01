namespace Crm.Data.Entities.Accounts;

public class Account
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string AccountNumber { get; private set; }
    public string? ShortName { get; private set; }
    public string? RegistrationType { get; private set; }
    public DateOnly? SetupDate { get; private set; }
    public DateOnly? ClosedDate { get; private set; }
    public string SponsorCompany { get; private set; }
    public bool Managed { get; private set; }
    public string? Type { get; private set; }
    public AccountStatus Status { get; private set; }
    public AccountDiscretion? Discretion { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    // Navigation properties
    public AccountBalance Balance { get; private set; } = null!;
    public ICollection<AccountOwner> AccountOwners { get; private set; } = new List<AccountOwner>();

    public Account(
        string id,
        string name,
        string accountNumber,
        string sponsorCompany,
        string? shortName = null,
        string? registrationType = null,
        DateOnly? setupDate = null,
        bool managed = false,
        string? type = null,
        AccountStatus status = AccountStatus.PendingApproval,
        AccountDiscretion? discretion = null)
    {
        Id = id;
        Name = name;
        AccountNumber = accountNumber;
        ShortName = shortName;
        RegistrationType = registrationType;
        SetupDate = setupDate;
        SponsorCompany = sponsorCompany;
        Managed = managed;
        Type = type;
        Status = status;
        Discretion = discretion;
    }

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(
        string name,
        string? shortName,
        string? registrationType,
        string? type,
        bool managed,
        AccountDiscretion? discretion)
    {
        Name = name;
        ShortName = shortName;
        RegistrationType = registrationType;
        Type = type;
        Managed = managed;
        Discretion = discretion;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(AccountStatus status, DateOnly? closedDate = null)
    {
        Status = status;
        if (status == AccountStatus.Closed)
        {
            ClosedDate = closedDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
        }
        UpdatedAt = DateTime.UtcNow;
    }
}