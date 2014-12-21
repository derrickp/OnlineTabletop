using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class Player
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }

        public DateTime JoinDate { get; set; }

        public List<Character> Characters { get; set; }

        public Player()
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }
}