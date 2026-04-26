using System;
using System.Collections.Generic;
using System.IO;

namespace Jaka_to_melodia
{
    public class SongManager
    {
        public List<Song> LoadSongsFromDirectory(string path)
        {
            List<Song> loadedSongs = new List<Song>();

            if (System.IO.Directory.Exists(path))
            {
                string[] files = System.IO.Directory.GetFiles(path, "*.mp3");

                foreach (string file in files)
                {
                    try
                    {
                        var tagFile = TagLib.File.Create(file);

                        string title = tagFile.Tag.Title ?? System.IO.Path.GetFileNameWithoutExtension(file);
                        string artist = tagFile.Tag.FirstPerformer ?? "Nieznany Wykonawca";

                        loadedSongs.Add(new Song(title, artist, file));
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                }
            }

            return loadedSongs;
        }
    }
}