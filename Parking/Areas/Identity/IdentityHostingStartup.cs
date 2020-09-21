using System;
using Parking.EF;
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Parking.Areas.Identity.IdentityHostingStartup))]
namespace Parking.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}