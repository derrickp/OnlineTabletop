using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class Player: IEntity
    {
        public string _id { get; set; }

        public string AccountName { get; set; }

        public string DisplayName { get; set; }
        
        public string Email { get; set; }
        
        public IList<string> CharacterIds { get; set; }

        public DateTime JoinDate { get; set; }

        public Player()
        {
        }
    }
}