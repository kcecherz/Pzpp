namespace Jaka_to_melodia
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string FilePath { get; set; }

        public string DisplayInfo => $"{Artist} - {Title}";

        public Song(string title, string artist, string filePath)
        {
            Title = title;
            Artist = artist;
            FilePath = filePath;
        }
    }

}