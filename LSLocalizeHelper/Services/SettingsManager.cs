using System;

using Alphaleonis.Win32.Filesystem;

using LSLocalizeHelper.Models;

using Newtonsoft.Json;

namespace LSLocalizeHelper.Services
{
    internal static class SettingsManager
    {
        private static readonly string                        settingsPath;

        static SettingsManager()
        {
            SettingsManager.settingsPath = SettingsManager.GetLocalFilePath("UserSettings.json");
            SettingsManager.Settings     = new UserSettings();
        }

        public static UserSettings? Settings { get; set; }

        private static string GetLocalFilePath(string fileName)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appData, "LSLocalizeHelper", fileName);
        }

        public static void Load()
        {
            if (File.Exists(SettingsManager.settingsPath))
                SettingsManager.Settings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(SettingsManager.settingsPath));
        }

        public static void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName( SettingsManager.settingsPath));
            var json = JsonConvert.SerializeObject(SettingsManager.Settings);
            File.WriteAllText(SettingsManager.settingsPath, json);
        }
    }
}
