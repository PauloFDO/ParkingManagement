using Microsoft.Extensions.Configuration;
using System.IO;

namespace ApplicationSettings
{
    public static class ManageAppSettingFileAccess
    {
        public static IConfiguration GetTheAppSettingInformation()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            return builder.Build();
        }
    }
}
