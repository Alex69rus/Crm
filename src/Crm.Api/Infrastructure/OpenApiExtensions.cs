namespace Crm.Api.Infrastructure;

public static class OpenApiExtensions
{
    public static IApplicationBuilder MapOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        return app;
    }
}