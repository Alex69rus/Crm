using Crm.Api.GraphQL.Queries;
using Crm.Core.Modules.Accounts.Queries;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;
using Crm.Data.Infrastructure;
using Npgsql;
using OpenTelemetry.Logs;
namespace Crm.Api;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure Serilog
        var loggerConfiguration = new LoggerConfiguration();

        // Get logging configuration from appsettings.json
        configuration.GetSection("Logging").GetSection("Serilog").Bind(loggerConfiguration);

        Log.Logger = loggerConfiguration
            .Enrich.FromLogContext()
            .CreateLogger();

        // Add OpenTelemetry
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName: "Crm.Api"))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                .AddOtlpExporter())
            .WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation())
            .WithLogging(c=> c.AddOtlpExporter());

        // Add OpenAPI services
        services.AddOpenApi();

        // Add GraphQL services
        services.AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<AccountQueries>();
        // Add MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(SearchAccounts).Assembly)
        );

        // Add DbContext
        services.AddCrmData(configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure metrics endpoints
        app.UseMetricServer();
        app.UseHttpMetrics();

        // Add GraphQL endpoint
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });
    }
}