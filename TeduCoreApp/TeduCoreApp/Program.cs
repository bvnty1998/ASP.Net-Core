using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeduCoreApp.EF;

namespace TeduCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                   
                    var dbInitializer = service.GetService<DbIntializer>();
                    dbInitializer.seed().Wait();
                }
                catch(Exception ex)
                {
                    var logger = service.GetService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while sedding the database ");
                }
            }
                host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
