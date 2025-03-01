using Crm.Api;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Formatting.Json;
using Crm.Data;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console(new JsonFormatter())
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);


startup.ConfigureServices(builder.Services);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails(
        new DestructuringOptionsBuilder()
            .WithDefaultDestructurers()
            .WithDestructurers([new DbUpdateExceptionDestructurer()])
    )
);


var app = builder.Build();
startup.Configure(app, builder.Environment);


try
{
    var configuration = app.Services.GetRequiredService<IConfiguration>();

    switch (configuration["Application:Mode"]?.ToLower())
    {
        case "migrate":
        {
            var dbContext = CrmDbContextFactory.CreateDbContext(configuration);
            await dbContext.Database.MigrateAsync();

            break;
        }
        case "serve":
        case "swagger":
            app.Run();
            break;

        default:
        {
            var dbContext = CrmDbContextFactory.CreateDbContext(configuration);
            await dbContext.Database.MigrateAsync();

            app.Run();
            break;
        }
    }

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
