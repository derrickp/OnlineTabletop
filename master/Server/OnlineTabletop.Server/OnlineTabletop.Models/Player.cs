using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class Player: IEntity
    {
        public string _id { get; set; }

        public string accountName { get; set; }

        public string displayName { get; set; }
        
        public string email { get; set; }
        
        public IList<string> characterIds { get; set; }

        public DateTime joinDate { get; set; }

        public Player()
        {
        }
    }
}