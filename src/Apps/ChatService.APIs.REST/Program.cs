using ChatService.APIs.REST;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiCors()
    .AddVersioning()
    .AddEndpointsApiExplorer()
    .AddSwagger();

builder.Services.AddControllers();
builder.AddServiceDefaults();

var app = builder.Build();

var serviceProvider = app.Services;
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

try
{
    _ = app.MapDefaultEndpoints();

    if (!app.Environment.IsProduction())
    {
        _ = app.UseDeveloperExceptionPage();
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI();
    }

    _ = app.UseHttpsRedirection();

    _ = app.UseCors("CorsPolicy");

    _ = app.UseAuthorization();

    _ = app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.LogError(e, "Could not run service");
}