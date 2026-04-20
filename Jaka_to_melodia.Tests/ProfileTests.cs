using Xunit;
using Jaka_to_melodia;

namespace Jaka_to_melodia.Tests
{
    public class ProfileTests
    {
        [Fact]
        public void NewProfile_ShouldHaveZeroHighscore_And_GeneratedId()
        {
            Profile newPlayer = new Profile("Kacper");

            Assert.Equal("Kacper", newPlayer.Name);
            Assert.Equal(0, newPlayer.Highscore); 

            Assert.False(string.IsNullOrEmpty(newPlayer.Id));
        }
    }
}