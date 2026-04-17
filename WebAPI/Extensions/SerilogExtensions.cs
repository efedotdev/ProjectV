using Microsoft.Identity.Client;
using Serilog;
namespace WebAPI.Extensions
{
    public static class SerilogExtensions
    {
        public static void AddSerilogConfiguration(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Async(a => a.File("Logs/log-.txt", rollingInterval: RollingInterval.Day))
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
