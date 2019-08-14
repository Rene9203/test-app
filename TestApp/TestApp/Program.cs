using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestApp.Infraestructure.DbConfig;
using TestApp.Core.Entities;
using TestApp;
using TestApp.Identity;
using TestApp.Core.Entities.Repositories;
using TestApp.Infraestructure.Repositories;
using TestApp.Infraestructure.Data;
using System.Linq;
using System;
using Microsoft.AspNetCore;

namespace TestApp
{
    public class Program
    {
        public static bool Seeding = false;
        public static void Main(string[] args)
        {
            Seeding = args.Contains("seed=True");
            IWebHost host = CreateWebHostBuilder(args).Build();
            if (Seeding)
            {
                Seed(host);
            }
            else
                host.Run();
        }

        private static async void Seed(IWebHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    logger.LogError("Seeding data base....");
                    AppDbContext context = services.GetService<AppDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();

                    var adminUser = CreateAdminUser(services, context);
                    var employerUser = CreateEmployerUser(services, context);
                    if (SeedData.SeedDelepmentData(context, adminUser.Id, employerUser.Id))
                        logger.LogError("Data base sedded successfully");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            }
        }

        private static AppUser CreateAdminUser(IServiceProvider services, AppDbContext appDbContext)
        {
            IEmployerRepository employerRespository = new EmployerRepository(appDbContext);
            UserManager<AppUser> userManager = services.GetService<UserManager<AppUser>>();

            AppUser johnDoe = new AppUser()
            {
                Email = "john.doe@email.com",
                FirstName = "John",
                LastName = "Doe",
                UserName = "john.doe@email.com"
            };

            if (userManager.CreateAsync(johnDoe, "J0hnD03Pa$$").Result.Succeeded)
                return johnDoe;

            return null;
        }

        private static AppUser CreateEmployerUser(IServiceProvider services, AppDbContext appDbContext)
        {
            IEmployerRepository employerRespository = new EmployerRepository(appDbContext);
            UserManager<AppUser> userManager = services.GetService<UserManager<AppUser>>();

            AppUser johnDoe = new AppUser()
            {
                Email = "jane.doe@email.com",
                FirstName = "Jane",
                LastName = "Doe",
                UserName = "jane.doe@email.com"
            };

            if (userManager.CreateAsync(johnDoe, "J4n3D03Pa$$").Result.Succeeded)
                return johnDoe;

            return null;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseKestrel()
                .UseStartup<Startup>();
    }
}
