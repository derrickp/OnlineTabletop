using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Race Race { 
            get
            {
                return Race;
            }
            set
            {
                if (value != null)
                {
                    Race = value;
                    if (Size == null)
                    {
                        Size = Race.Size;
                    }
                }
            }
        }

        public double Weight { get; set; }
        public double Height { get; set; }
        public Alignments Alignment { get; set; }
        public string Deity { get; set; }

        /// <summary>
        /// Kept separate from Race because spells could also impact a characters size.
        /// </summary>
        public Size Size { get; set; }

        // These are the characters base abilities. Only modified on creation and occasional levelling.
        public Ability Strength { get; set; }
        public Ability Dexterity { get; set; }
        public Ability Constitution { get; set; }
        public Ability Intelligence { get; set; }
        public Ability Wisdom { get; set; }
        public Ability Charisma { get; set; }
        
        public int TotalHP { get; set; }
        public int CurrentHP { get; set; }
        public int NonLethalDamage { get; set; }

        public int BaseMovementSpeed { get; set; }
        public int BaseAttackBonus { get; set; }

        public int CMB
        {
            get
            {
                return this.BaseAttackBonus + Strength.Modifier + Size.Modifier;
            }
        }

        public int CMD
        {
            get
            {
                return this.BaseAttackBonus + Strength.Modifier + Dexterity.Modifier + 10;
            }
        }

        public List<string> KnownLanguages { get; set; }

        public List<Class> Classes { get; set; }

        /// <summary>
        /// The Player that this character belongs to.
        /// </summary>
        public Player Player { get; set; }

        public Character()
        {
            
        }

        public Character(string name, Player player)
        {
            Name = name;
            Player = player;
        }

        public Character(string name, Player player, Race race)
        {
            Name = name;
            Player = player;
            Race = race;
        }

        /// <summary>
        /// Retrieves the full character level. This is the combination of all classes
        /// </summary>
        /// <returns>Integer for character level</returns>
        public int CharacterLevel()
        {
            int level = 0;
            foreach (Class charClass in Classes)
            {
                level += charClass.Level;
            }
            return level;
        }

        /// <summary>
        /// Given a class, get the level this character is in that class.
        /// </summary>
        /// <param name="inClass">The class to check</param>
        /// <returns>Integer of specified class level</returns>
        public int ClassLevel(Class inClass)
        {
            return Classes.Count(x => x.Name == inClass.Name);
        }

        /// <summary>
        /// Given a class name, what is the level of that character in that class.
        /// </summary>
        /// <param name="className">The class name</param>
        /// <returns>Integer of specified class level</returns>
        public int ClassLevel(string className)
        {
            return Classes.Count(x => x.Name == className);
        }
    }
}