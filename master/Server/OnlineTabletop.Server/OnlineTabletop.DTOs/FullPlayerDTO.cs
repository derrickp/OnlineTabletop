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

        public IList<BasicCharacterDTO> basicCharacters { get; set; }

        public FullPlayerDTO()
        {
            basicCharacters = new List<BasicCharacterDTO>();
        }
    }
}
