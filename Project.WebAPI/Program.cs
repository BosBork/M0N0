using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;

namespace Project.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggerConfig();
            try
            {
                Log.Information("Application Is Starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application Failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static Logger LoggerConfig()
        {
            return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.File(
                path: Path.Combine(Environment.CurrentDirectory, "WebApiLogs", "log-.txt"),
                //path: Path.Combine(AppContext.BaseDirectory, "WebApiLogs", "log-.txt"),
                //outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information
            )
            .CreateLogger();
        }

    }
}
