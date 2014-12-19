using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }

        public List<Character> Characters { get; set; }

        public Player GM { get; set; }

        public Room()
        {

        }
    }
}