using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class RollsDTO
    {
        public IDictionary<string, int> diceRolls { get; set; }

        public RollsDTO()
        {
            diceRolls = new Dictionary<string, int>();
        }
    }
}
