using System;
using System.Collections.Generic;
using System.IO;

namespace Jaka_to_melodia
{
    public class SongManager
    {
        public List<Song> LoadSongsFromDirectory(string directoryPath)
        {
            List<Song> songs = new List<Song>();

            if (!Directory.Exists(directoryPath))
            {
                return songs;
            }

            string[] files = Directory.GetFiles(directoryPath, "*.mp3", SearchOption.AllDirectories);

            foreach (string file in files)
            {

                var tfile = TagLib.File.Create(file);

                string title = string.IsNullOrEmpty(tfile.Tag.Title)
                    ? Path.GetFileNameWithoutExtension(file)
                    : tfile.Tag.Title;

                string artist = "Nieznany wykonawca";

                if (tfile.Tag.Performers != null && tfile.Tag.Performers.Length > 0)
                {
                    artist = string.Join(", ", tfile.Tag.Performers);
                }
                else if (tfile.Tag.AlbumArtists != null && tfile.Tag.AlbumArtists.Length > 0)
                {
                    artist = string.Join(", ", tfile.Tag.AlbumArtists);
                }
                songs.Add(new Song(title, artist, file));
            }

            return songs;
        }
    }
}