namespace Crm.Data.Entities.Accounts;

public class AccountBalance(string id, string accountId, decimal balance)
{
    public string Id { get; private set; } = id;
    public decimal Balance { get; private set; } = balance;
    public DateTime LastUpdatedAt { get; private set; } = DateTime.UtcNow;

    // Foreign key
    public string AccountId { get; private set; } = accountId;
    public Account Account { get; private set; } = null!;
} 