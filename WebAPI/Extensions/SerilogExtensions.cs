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
                .WriteTo.Async(a => a.File(
                   path: "Logs/log-.txt",
                   rollingInterval: RollingInterval.Day,
                   retainedFileCountLimit: 15,
                   fileSizeLimitBytes: 10 * 1024 * 1024,
                   rollOnFileSizeLimit: true
                ))
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
