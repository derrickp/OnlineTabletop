using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class RpgClass
    {
        public string Name { get; set; }

        public int HitDice { get; set; }

        public int FortitudeBaseSave { get; set; }

        public int ReflexBaseSave { get; set; }

        public int WillBaseSave { get; set; }

        public List<int> BaseAttacks { get; set; }

        public int Level { get; set; }

        public RpgClass()
        {

        }
    }
}