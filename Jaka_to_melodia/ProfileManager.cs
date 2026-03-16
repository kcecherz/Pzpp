using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Jaka_to_melodia
{
    public class ProfileManager
    {

        private readonly string filePath = "profiles.json";

        public void SaveProfiles(List<Profile> profiles)
        {

            var options = new JsonSerializerOptions { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(profiles, options);

            File.WriteAllText(filePath, jsonString);
        }

        public List<Profile> LoadProfiles()
        {

            if (!File.Exists(filePath))
            {
                return new List<Profile>(); 
            }

            string jsonString = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Profile>>(jsonString);
        }
    }
}
