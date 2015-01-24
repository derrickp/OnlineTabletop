using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTabletop.Models
{
    public class Character: IEntity
    {
        #region Properties
        public string _id { get; set; }

        public string Name { get; set; }

        public string Race { get; set; }

        public double Weight { get; set; }
        public double Height { get; set; }
        public string Alignment { get; set; }
        public string Deity { get; set; }

        /// <summary>
        /// Kept separate from Race because spells could also impact a characters size.
        /// </summary>
        public string Size { get; set; }
        public int SizeModifier { get; set; }

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

        public List<string> KnownLanguages { get; set; }

        public List<RpgClass> Classes { get; set; }

        /// <summary>
        /// The Player that this character belongs to.
        /// </summary>
        public string PlayerId { get; set; }
        #endregion

        #region Derived character properties
        /// <summary>
        /// Retrieves the full character level. This is the combination of all classes
        /// </summary>
        /// <returns>Integer for character level</returns>
        public int GetCharacterLevel()
        {
            int level = 0;
            foreach (RpgClass charClass in Classes)
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
        public int GetClassLevel(RpgClass inClass)
        {
            return Classes.Count(x => x.Name == inClass.Name);
        }

        /// <summary>
        /// Given a class name, what is the level of that character in that class.
        /// </summary>
        /// <param name="className">The class name</param>
        /// <returns>Integer of specified class level</returns>
        public int GetClassLevel(string className)
        {
            return Classes.Count(x => x.Name == className);
        }

        public List<int> GetBaseAttackBonus()
        {
            List<int> attackBonuses = new List<int>();
            attackBonuses.Add(0);

            foreach (RpgClass charClass in Classes)
            {
                for (int i = 0; i < charClass.BaseAttacks.Count; i++)
                {
                    if (i > attackBonuses.Count - 1)
                    {
                        attackBonuses.Add(charClass.BaseAttacks[i]);
                    }
                    else
                    {
                        attackBonuses[i] += charClass.BaseAttacks[i];
                    }
                }
            }
            return attackBonuses;
        }

        public int GetFortitudeBaseSave()
        {
            int save = 0;
            foreach (RpgClass charClass in Classes)
            {
                save += charClass.FortitudeBaseSave;
            }
            // Need to find some way to add in other random modifiers.
            save += Constitution.GetModifier();
            return save;
        }

        public int GetReflexBaseSave()
        {
            int save = 0;
            foreach (RpgClass charClass in Classes)
            {
                save += charClass.ReflexBaseSave;
            }
            save += Dexterity.GetModifier();
            return save;
        }

        public int GetWillBaseSave()
        {
            int save = 0;

            foreach (RpgClass charClass in Classes)
            {
                save += charClass.WillBaseSave;
            }
            save += Wisdom.GetModifier();
            return save;
        }

        public int GetCMB()
        {
            if (this.GetBaseAttackBonus().Any()) return this.GetBaseAttackBonus().First() + Strength.GetModifier() + SizeModifier;
            return 0;
        }

        public int GetCMD()
        {
            if (this.GetBaseAttackBonus().Any()) return this.GetBaseAttackBonus().First() + Strength.GetModifier() + Dexterity.GetModifier() + 10;
            return 0;
        }
        #endregion

        #region Constructors
        public Character()
        {

        }

        public Character(string name, Player player)
        {
            Name = name;
            PlayerId = player._id;
        }

        public Character(string name, Player player, Race race)
        {
            Name = name;
            PlayerId = player._id;
            Race = race.Name;
            Size = race.Size.Type;
            SizeModifier = race.Size.Modifier;
            
        }
        #endregion
    }
}