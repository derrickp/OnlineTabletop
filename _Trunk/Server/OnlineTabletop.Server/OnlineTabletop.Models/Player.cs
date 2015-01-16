using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class Player: IEntity
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        //public DateTime joinDate { get; set; }

        public List<Character> characters { get; set; }

        public Player()
        {
        }
    }
}