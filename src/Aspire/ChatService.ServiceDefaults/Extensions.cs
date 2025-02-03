using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ChatService.ServiceDefaults;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class Extensions
{
    public const string OTEL_EXPORTER_OTLP_ENDPOINT_ENV_NAME = "OTEL_EXPORTER_OTLP_ENDPOINT";

    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        _ = builder.ConfigureOpenTelemetry();

        _ = builder.AddDefaultHealthChecks();

        _ = builder.Services.AddServiceDiscovery();

        _ = builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            _ = http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            _ = http.AddServiceDiscovery();
        });

        // Uncomment the following to restrict the allowed schemes for service discovery.
        // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
        // {
        //     options.AllowedSchemes = ["https"];
        // });

        return builder;
    }

    /// <summary>
    /// Configures OpenTelemetry for metrics/tracing and sets up Serilog with an OpenTelemetry sink for logging.
    /// </summary>
    //public static TBuilder ConfigureOpenTelemetryWithSerilog<TBuilder>(this TBuilder builder)
    //    where TBuilder : IHostApplicationBuilder
    //{
    //    _ = builder.Logging.ClearProviders();

    //    var otlpEndpoint = builder.Configuration[OTEL_EXPORTER_OTLP_ENDPOINT_ENV_NAME];

    //    Log.Logger = new LoggerConfiguration()
    //        .MinimumLevel.Information()
    //        //.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
    //        //.MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Warning)
    //        //.MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker", LogEventLevel.Warning)
    //        //.MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor", LogEventLevel.Warning)
    //        //.MinimumLevel.Override("Serilog.AspNetCore.RequestLoggingMiddleware", LogEventLevel.Warning)
    //        .Enrich.FromLogContext()
    //        .Enrich.WithProperty("deployment.environment", builder.Environment.EnvironmentName)
    //        // Optional: Enrich trace/span info from Serilog.Enrichers.OpenTelemetry
    //        // .Enrich.With<YourOpenTelemetryEnricher>()
    //        // todo - Write to console only if development
    //        .WriteTo.Console()
    //        .WriteTo.OpenTelemetry(x =>
    //        {
    //            x.Endpoint = otlpEndpoint;
    //            x.Protocol = OtlpProtocol.HttpProtobuf;
    //        })
    //        .CreateLogger();

    //    _ = builder.ConfigureOpenTelemetryMetricsAndTracing();

    //    return builder;
    //}

    public static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        _ = builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;

            _ = logging.SetResourceBuilder(
                ResourceBuilder.CreateDefault().AddAttributes(new Dictionary<string, object>()
                {
                    ["deployment.environment"] = builder.Environment.EnvironmentName
                }));
        });

        _ = builder.ConfigureOpenTelemetryMetricsAndTracing();
        _ = builder.AddOpenTelemetryExporters();

        // Set filters
        //_ = builder.Logging.AddFilter("Microsoft.AspNetCore.Hosting.Diagnostics", LogLevel.Warning);
        //_ = builder.Logging.AddFilter("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogLevel.Warning);
        //_ = builder.Logging.AddFilter("Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker", LogLevel.Warning);
        //_ = builder.Logging.AddFilter("Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor", LogLevel.Warning);
        //_ = builder.Logging.AddFilter("Serilog.AspNetCore.RequestLoggingMiddleware", LogLevel.Warning);

        return builder;
    }

    public static TBuilder ConfigureOpenTelemetryMetricsAndTracing<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        _ = builder.Services.AddOpenTelemetry()
        .WithMetrics(metrics =>
        {
            _ = metrics.AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation();
        })
        .WithTracing(tracing =>
        {
            _ = tracing.AddSource(builder.Environment.ApplicationName)
                .AddAspNetCoreInstrumentation()
                // Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
                //.AddGrpcClientInstrumentation()
                .AddHttpClientInstrumentation();
        });

        return builder;
    }

    private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration[OTEL_EXPORTER_OTLP_ENDPOINT_ENV_NAME]);

        if (useOtlpExporter)
            _ = builder.Services.AddOpenTelemetry().UseOtlpExporter();

        // Uncomment the following lines to enable the Azure Monitor exporter (requires the Azure.Monitor.OpenTelemetry.AspNetCore package)
        //if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
        //{
        //    builder.Services.AddOpenTelemetry()
        //       .UseAzureMonitor();
        //}

        return builder;
    }

    public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        _ = builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            // All health checks must pass for app to be considered ready to accept traffic after starting
            _ = app.MapHealthChecks("/health");

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            _ = app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }

        return app;
    }
}