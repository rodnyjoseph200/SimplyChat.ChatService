using Asp.Versioning;

namespace ChatService.APIs.REST;

internal static class ServiceCollectionExtensions
{
    internal const string CORS_POLICY_NAME = "CorsPolicy";
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy(CORS_POLICY_NAME, builder =>
            {
                _ = builder
                    // todo rod - include frontend url
                    //.WithOrigins("http://localhost:3000")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                //.AllowCredentials();
            });
        });
    }

    internal static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        _ = services
        .AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("X-Version"));
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        return services;
    }

    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.
            AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });
    }
}