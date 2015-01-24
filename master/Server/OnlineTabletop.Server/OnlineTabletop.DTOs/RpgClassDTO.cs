using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class RpgClassDTO
    {
        public string name { get; set; }
        public int level { get; set; }
        public int hitDice { get; set; }
        public int fortitudeBaseSave { get; set; }
        public int reflexBaseSave { get; set; }
        public int willBaseSave { get; set; }
        public List<int> baseAttacks { get; set; }

        public RpgClassDTO()
        {
        }
    }
}
