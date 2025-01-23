using Asp.Versioning;

namespace ChatService.APIs.REST;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiCors(this IServiceCollection services)
    {
        _ = services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
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
        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
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

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        _ = services
            .AddSwaggerGen();
        return services;
    }
}