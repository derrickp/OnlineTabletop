using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class FullCharacterDTO
    {
        public string id { get; set; }

        [Required]
        public string name { get; set; }

        public string playerAccountName { get; set; }

        public List<RpgClassDTO> classes { get; set; }

        public string race { get; set; }

        public double weight { get; set; }

        public double height { get; set; }

        public string alignment { get; set; }

        public string deity { get; set; }

        public string size { get; set; }

        public int sizeModifier { get; set; }

        public List<AbilityDTO> abilities { get; set; }

        public FullCharacterDTO()
        {

        }
    }
}
