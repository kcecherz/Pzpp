using System.IO;
using Xunit;
using Jaka_to_melodia;

namespace Jaka_to_melodia.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void SongManager_ShouldSkipCorruptedFiles_WithoutCrashing()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), "JakaToMelodia_Testy_Corrupt");
            Directory.CreateDirectory(tempDirectory);

            string dummyMp3Path = Path.Combine(tempDirectory, "zepsuta_piosenka.mp3");
            File.WriteAllText(dummyMp3Path, "To nie jest prawdziwe audio!");

            SongManager manager = new SongManager();

            try
            {
                var loadedSongs = manager.LoadSongsFromDirectory(tempDirectory);
                Assert.Empty(loadedSongs);
            }
            finally
            {
                if (Directory.Exists(tempDirectory))
                {
                    Directory.Delete(tempDirectory, true);
                }
            }
        }
    }
}