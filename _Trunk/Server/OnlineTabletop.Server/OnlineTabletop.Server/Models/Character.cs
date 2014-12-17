using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Server.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Race { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public Alignments Alignment { get; set; }
        public string Deity { get; set; }

        public int HitDice { get; set; }

        public int TotalHP { get; set; }
        public int CurrentHP { get; set; }
        public int NonLethalDamage { get; set; }

        public int BaseMovementSpeed { get; set; }

        public List<string> KnownLanguages { get; set; }
        public List<Class> Classes { get; set; }

        public Player Player { get; set; }

        public Character()
        {
            
        }

        public int CharacterLevel()
        {
            return Classes.Count;
        }

        public int ClassLevel(Class inClass)
        {
            return Classes.Count(x => x.Name == inClass.Name);
        }

        public int ClassLevel(string className)
        {
            return Classes.Count(x => x.Name == className);
        }
    }
}