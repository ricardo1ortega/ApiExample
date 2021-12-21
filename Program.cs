using ApiExample.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample
{
    public class Program
    {
        private static string _environment { get; set; }
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            ProgramSettings.SetEnvironment(_environment);
            var connection = ProgramSettings.LoggerConectionHandler(configuration, "ServicesDb");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .WriteTo.Console()
                .WriteTo.MongoDB(connection, collectionName: "Applog", restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            try
            {
                Log.Information($"Starting Service in mode {_environment}");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
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
        private static IConfiguration GetConfiguration()
        {
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationBuilder config = new ConfigurationBuilder();

            return config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{_environment}.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}
