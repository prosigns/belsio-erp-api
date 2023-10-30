using Belsio.Erp.Application;
using Belsio.Erp.Host.Configurations;
using Belsio.Erp.Host.Controllers;
using Belsio.Erp.Infrastructure;
using Belsio.Erp.Infrastructure.Common;
using Belsio.Erp.Infrastructure.Logging.Serilog;
using Serilog;
using Serilog.Formatting.Compact;

[assembly: ApiConventionType(typeof(BaseApiConventions))]

StaticLogger.EnsureInitialized();
Log.Information("Belsio ERP :: Server Booting Up...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddConfigurations().RegisterSerilog();
    builder.Services.AddControllers();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    var app = builder.Build();

    await app.Services.InitializeDatabasesAsync();

    app.UseInfrastructure(builder.Configuration);
    app.MapEndpoints();
    app.Use(async (context, next) =>
    {
        context.Request.EnableBuffering();
        await next();
    });
    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Belsio ERP :: Server start-up failed");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Belsio ERP :: Server Shutting down...");
    Log.CloseAndFlush();
}