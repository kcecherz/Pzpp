using System.IO;
using System.Text.Json;

namespace Jaka_to_melodia
{
    public class SettingsManager
    {
        private readonly string filePath = "settings.json";

        public void SaveSettings(AppSettings settings)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(filePath, jsonString);
        }

        public AppSettings LoadSettings()
        {
            if (!File.Exists(filePath))
            {
                return new AppSettings { MusicFolderPath = "" };
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<AppSettings>(jsonString);
        }
    }
}