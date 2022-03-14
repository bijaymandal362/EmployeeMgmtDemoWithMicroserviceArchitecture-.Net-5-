using Demo.Entities.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Demo.API
{
    public class Program
    {
        public  static  void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<DemoDbContext>();
                    var isMigrationAvailable = db.Database.GetPendingMigrations().ToList();
                    if (isMigrationAvailable.Count>0)
                    {
                        db.Database.Migrate();
                    }
                  
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
