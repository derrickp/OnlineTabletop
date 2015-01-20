using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class AbilityDTO
    {
        public string name { get; set; }

        public int score { get; set; }

        public int tempAdjustment { get; set; }

        public int modifier { get; set; }

        public AbilityDTO()
        {

        }
    }
}
