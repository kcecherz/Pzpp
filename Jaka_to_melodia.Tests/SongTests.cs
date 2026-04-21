using Xunit;
using Jaka_to_melodia; 

namespace Jaka_to_melodia.Tests
{
    public class SongTests
    {
        [Fact] 
        public void DisplayInfo_ShouldFormatCorrectly()
        {
            string expectedTitle = "Faded";
            string expectedArtist = "Alan Walker";
            Song testSong = new Song(expectedTitle, expectedArtist, "C:\\fake_path.mp3");

            string result = testSong.DisplayInfo;

            Assert.Equal("Alan Walker - Faded", result);
        }
    }
}