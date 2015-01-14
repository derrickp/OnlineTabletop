using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class FullPlayerDTO
    {
        public string id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public IList<BasicCharacterDTO> characters { get; set; }

        public FullPlayerDTO()
        {
            characters = new List<BasicCharacterDTO>();
        }
    }
}
