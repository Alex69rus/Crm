using Crm.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace Crm.Data;

public class CrmDbContextFactory : IDesignTimeDbContextFactory<CrmDbContext>
{
    public CrmDbContext CreateDbContext(string[] args)
    {
        if (args.Length == 0)
            args = ["dbmigration.settings.json"];

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(args[0], true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(nameof(CrmDbContext))
            ?? throw new InvalidOperationException($"Connection string '{nameof(CrmDbContext)}' not found.");

        return new CrmDbContext(new DbContextOptionsBuilder<CrmDbContext>().UseNpgsql(connectionString).Options);
    }

    public static CrmDbContext CreateDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(CrmDbContext));
        return CreateDbContext(connectionString!);
    }

    public static CrmDbContext CreateDbContext(string connectionString)
        => new(
            new DbContextOptionsBuilder<CrmDbContext>()
                .UseNpgsql(connectionString, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, DbSchemas.Crm))
                .Options
        );


}