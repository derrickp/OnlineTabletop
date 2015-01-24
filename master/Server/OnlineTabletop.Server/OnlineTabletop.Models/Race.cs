using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Models
{
    public class Race
    {
        public string Name { get; set; }

        public string PhysicalDescription { get; set; }

        public string Society { get; set; }

        public string Relations { get; set; }

        public string AlignmentAndReligion { get; set; }

        public string Adventurers { get; set; }

        public Dictionary<string, int> AbilityModifications { get; set; }

        public Size Size { get; set; }
    }
}
