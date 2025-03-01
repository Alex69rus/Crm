using Crm.Data.Entities.Contacts;

namespace Crm.Data.Entities.Accounts;

public class AccountOwner
{
    public string AccountId { get; private set; }
    public string ContactId { get; private set; }
    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;

    // Navigation properties
    public Account Account { get; private set; } = null!;
    public Contact Contact { get; private set; } = null!;

    public AccountOwner(string accountId, string contactId)
    {
        AccountId = accountId;
        ContactId = contactId;
    }
}