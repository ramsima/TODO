 using Serilog;
using System.Reflection;
using Todo.Application.DependencyInjection;
using Todo.Infrastructure.DependencyInjection;
using TODO.API.Middleware;
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    //builder.Host.UseSerilog((context,services,configuration) =>
    //    configuration.ReadFrom.Configuration(context.Configuration)
    //    .ReadFrom.Services(services)
    //    .Enrich.FromLogContext().Enrich.
    //);

    //builder.Services.AddSerilog((services, lc) =>
    //    lc.ReadFrom.Configuration(builder.Configuration)
    //    .ReadFrom.Services(services)
    //    .Enrich.FromLogContext().WriteTo.Console()
    //);

    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                "Logs/log-.txt",
                rollingInterval: RollingInterval.Day
            );
    });

    //builder.Host.UseSerilog((x, y) =>
    //{
    //    y.ReadFrom.Configuration(x.Configuration)
    //    .Enrich.FromLogContext();

    //    y.WriteTo.File(path: "/Logs/log.txt");
    //    y.WriteTo.Console();
    //});

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplication();

    builder.Services.AddInfrastructure();

    

    var app = builder.Build();
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Application Starting Up...");

    app.UseMiddleware<GlobalExceptionMiddleware>();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    try
    {
        app.MapControllers();
    }
    catch (ReflectionTypeLoadException ex)
    {
        foreach (var loaderException in ex.LoaderExceptions)
        {
            Console.WriteLine(loaderException?.Message);
        }

        throw;
    }

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex,"Application Failed To StartUp!");
}
finally
{
    Log.CloseAndFlush();
}
