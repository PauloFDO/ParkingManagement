using ParkingSpace;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.shared
{
    public static class ShareAppSettingsAcrossProject
    {
        public static IWebHost BuildWebHost(string[] args) =>
          new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  var env = hostingContext.HostingEnvironment;

                  // find the shared folder in the parent folder
                  var sharedFolder = Path.Combine(env.ContentRootPath, "..", "Shared");

                  //load the SharedSettings first, so that appsettings.json overrwrites it
                  config
                    .AddJsonFile(Path.Combine(sharedFolder, "SharedSettings.json"), optional: true)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

                  config.AddEnvironmentVariables();
              })
              .UseDefaultServiceProvider((ctx, opts) =>
              {
                  opts.ValidateScopes = ctx.HostingEnvironment.IsDevelopment();
              })
              .UseStartup<Startup>()
              .Build();
    }
}
