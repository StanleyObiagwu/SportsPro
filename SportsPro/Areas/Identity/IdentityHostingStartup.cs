using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsPro.Data;

[assembly: HostingStartup(typeof(SportsPro.Areas.Identity.IdentityHostingStartup))]
namespace SportsPro.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<NewSportsProContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("NewSportsProContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<NewSportsProContext>();
            });
        }
    }
}