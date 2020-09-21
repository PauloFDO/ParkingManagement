using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ApplicationSettings
{
    public static class ConsumeAppSettingElements
    {
        public static string GetConnectionStringFromAppSetting()
        {
            var root = ManageAppSettingFileAccess.GetTheAppSettingInformation();
            return root.GetConnectionString(AppSettingsNames.DefaultConnection.ToString());
        }

        public static IConfigurationSection GetTheAzureOptionsForSSO()
        {
            var root = ManageAppSettingFileAccess.GetTheAppSettingInformation();
            return root.GetSection(AppSettingsNames.AzureAd.ToString());
        }

        public static bool IsAzureActive()
        {
            var root = ManageAppSettingFileAccess.GetTheAppSettingInformation();
            var valueInSettings = root.GetSection(AppSettingsNames.AzureEnabled.ToString());
            return Convert.ToBoolean(valueInSettings.Value);
        }
    }
}
