using System;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Crew.Api.ReferenceImpl.V1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string nlogConfigFileName = "nlog.config";

            /* use the env specific version of the nlog.config file - if it exists */
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            string envSpecificNLogConfigFileName = $"nlog.{environmentName}.config";

            if (File.Exists(envSpecificNLogConfigFileName))
            {
                nlogConfigFileName = envSpecificNLogConfigFileName;
            }

            /* setup the logger first to catch all errors */
            Logger logger = NLogBuilder.ConfigureNLog(nlogConfigFileName).GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");

                /* set the default culture */
                var defaultCulture = new CultureInfo("en-AU");

                CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
                CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex) // catch setup errors
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                /* ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux) */
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder
                            .ConfigureKestrel(opt => opt.AddServerHeader = false)
                            .UseStartup<Startup>();
                    })
                .ConfigureLogging(logging => logging.ClearProviders())
                .UseNLog(); // setup NLog for dependency injection
    }
}