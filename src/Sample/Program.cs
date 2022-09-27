using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var host = Host.CreateDefaultBuilder()
               .ConfigureLogging(builder =>
               {

               })
               .UseSerilog((builder, context) =>
               {
                   context.ReadFrom.Configuration(builder.Configuration)
                          .Enrich.FromLogContext();
               })
               .Build();

host.StartAsync();


var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();

var logger = loggerFactory.CreateLogger("Program");

logger.LogInformation("{@Hello}", new
{
    Hello = "World"
});

try
{
    throw new Exception("Error Here");
}
catch (Exception e)
{
    logger.LogError(e, "");
}




host.StopAsync();
