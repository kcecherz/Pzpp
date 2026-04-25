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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nazwa użytkownika nie może być pusta!");
            }

            Name = name;
            Id = Guid.NewGuid().ToString();
            Highscore = 0;
        }
    }
}