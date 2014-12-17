using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Server.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Race { get; set; }

        public List<string> Languages { get; set; }
        public List<string> Classes { get; set; }

        public Player Player { get; set; }

        public Character()
        {

        }

        public int CharacterLevel()
        {
            return Classes.Count;
        }
    }
}