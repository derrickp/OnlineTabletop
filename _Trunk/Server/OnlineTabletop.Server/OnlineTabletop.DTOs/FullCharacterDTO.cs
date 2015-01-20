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

        public string playerName { get; set; }

        [Required]
        public List<BasicRPGClassDTO> classes { get; set; }

        public string race { get; set; }

        public double weight { get; set; }

        public double height { get; set; }

        public string alignment { get; set; }

        public string deity { get; set; }

        public SizeDTO size { get; set; }

        [Required]
        public AbilityDTO strength { get; set; }

        [Required]
        public AbilityDTO dexterity { get; set; }

        [Required]
        public AbilityDTO constitution { get; set; }

        [Required]
        public AbilityDTO intelligence { get; set; }

        [Required]
        public AbilityDTO wisdom { get; set; }

        [Required]
        public AbilityDTO charisma { get; set; }

        [Required]
        public int characterLevel { get; set; }

        public FullCharacterDTO()
        {

        }
    }
}
