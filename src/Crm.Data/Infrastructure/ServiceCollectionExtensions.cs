using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crm.Data.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCrmData(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(CrmDbContext))
            ?? throw new InvalidOperationException($"Connection string '{nameof(CrmDbContext)}' not found.");

        services.AddDbContext<CrmDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}