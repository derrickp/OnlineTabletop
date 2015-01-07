using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class BasicCharacterDTO
    {
        public string id { get; set; }

        public string name { get; set; }

        public string playerName { get; set; }

        public List<BasicRPGClassDTO> classes { get; set; }

        public string race { get; set; }

        public int characterLevel { get; set; }

        public BasicCharacterDTO()
        {
        }
    }
}
