using Crm.Data.Entities.Accounts;
using Crm.Data.Entities.Contacts;
using Crm.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data;

public class CrmDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<AccountBalance> AccountBalances => Set<AccountBalance>();
    public DbSet<AccountOwner> AccountOwners => Set<AccountOwner>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<ContactAddress> ContactAddresses => Set<ContactAddress>();
    public DbSet<ContactPhone> ContactPhones => Set<ContactPhone>();

    public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set default schema
        modelBuilder.HasDefaultSchema(DbSchemas.Crm);

        // Apply configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrmDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use snake_case naming convention for PostgreSQL
        optionsBuilder.UseSnakeCaseNamingConvention();

        base.OnConfiguring(optionsBuilder);
    }
}