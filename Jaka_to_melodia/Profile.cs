using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaka_to_melodia
{
    public class Profile
    {
        public string Name { get; set; }
        public int Highscore { get; set; }
        public Profile() { }
        public Profile(string name)
        {
            Name = name;
            Highscore = 0; 
        }
    }
}
