namespace Crm.Api.Infrastructure;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        return services;
    }

    public static IApplicationBuilder MapOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        return app;
    }
} 