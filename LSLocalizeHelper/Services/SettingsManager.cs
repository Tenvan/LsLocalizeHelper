using System;

using Alphaleonis.Win32.Filesystem;

using Newtonsoft.Json;

namespace LSLocalizeHelper.Services
{
    internal class SettingsManager<T> where T : class, new()
    {
        private readonly string settingsPath;

        public SettingsManager()
        {
            this.settingsPath = this.GetLocalFilePath("UserSettings.json");
            this.Settings  = new T();
        }

        public T? Settings { get; set; }

        private string GetLocalFilePath(string fileName)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appData, "LSLocalizeHelper", fileName);
        }

        public void Load()
        {

            if (File.Exists(this.settingsPath))
                this.Settings = JsonConvert.DeserializeObject<T>(File.ReadAllText(this.settingsPath));
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName( this.settingsPath));
            var json = JsonConvert.SerializeObject(this.Settings);
            File.WriteAllText(this.settingsPath, json);
        }
    }
}
