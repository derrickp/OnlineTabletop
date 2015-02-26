using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class FullPlayerDTO
    {
        public string _id { get; set; }

        public string name { get; set; }

        public string displayName { get; set; }

        public DateTime joinDate { get; set; }

        public string email { get; set; }

        public IList<string> characterIds { get; set; }

        public FullPlayerDTO()
        {
            characterIds = new List<string>();
        }
    }
}
