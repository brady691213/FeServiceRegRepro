namespace Cts.Fe.System;

public static class ApiSwaggerExtensions
{
    public static void AddSwaggerDocument(this IServiceCollection services)
    {
        services.SwaggerDocument(
            d => d.DocumentSettings =
                     s =>
                     {
                         s.DocumentName = "v0";
                         s.Version = "0.0.0";
                     });
    }

    public static void AddSwaggerGen(this WebApplication app)
    {
        app.UseSwaggerGen(uiConfig: u => u.DeActivateTryItOut());
    }
}