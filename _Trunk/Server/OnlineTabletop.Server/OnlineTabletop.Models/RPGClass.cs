using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class RPGClass
    {
        public string Name { get; set; }

        public int HitDice { get; set; }

        public int FortitudeBaseSave { get; set; }

        public int ReflexBaseSave { get; set; }

        public int WillBaseSave { get; set; }

        public int BaseAttack { get; set; }

        public int Level { get; set; }

        public RPGClass()
        {

        }
    }
}