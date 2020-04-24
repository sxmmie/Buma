using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Buma.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Buma.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();     // build app

            try
            {
                using(var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    // Ensure DB is created
                    context.Database.EnsureCreated();

                    // check if any user exist
                    if (context.Users.Any())
                    {
                        // create admin user
                        var adminUser = new IdentityUser
                        {
                            UserName = "Admin"
                        };

                        // manager
                        var managerUser = new IdentityUser
                        {
                            UserName = "Manager"
                        };

                        userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                        userManager.CreateAsync(managerUser, "password").GetAwaiter().GetResult();

                        // How do you know whose an adminuser and whose managerUser? -> claims
                        // Claims are what the user has, used to describe the user (users contain claims), roles contain a user.
                        // A claim is a key-value pair, added 2 users and gave then claims that define their roles
                        var adminClaim = new Claim("Role", "Admin");
                        var managerClaim = new Claim("Role", "Manager");

                        userManager.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                        userManager.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();
                    }
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
                
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();                     // collect all services and put them a container for use
    }
}
