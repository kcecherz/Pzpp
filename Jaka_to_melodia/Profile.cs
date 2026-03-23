using System;

namespace Jaka_to_melodia
{
    public class Profile
    {

        public string Id { get; set; }

        public string Name { get; set; }
        public int Highscore { get; set; }

        public Profile() { }

        public Profile(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Highscore = 0;
        }
    }
}