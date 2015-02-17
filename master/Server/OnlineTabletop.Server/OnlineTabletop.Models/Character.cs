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
        
        // Making a generic list of abilities instead of putting each one in individually.
        public List<Ability> Abilities { get; set; }
        
        public int TotalHP { get; set; }
        public int CurrentHP { get; set; }
        public int NonLethalDamage { get; set; }

        public int BaseMovementSpeed { get; set; }

        public List<string> KnownLanguages { get; set; }

        public List<RpgClass> Classes { get; set; }

        /// <summary>
        /// The Player that this character belongs to.
        /// </summary>
        public string PlayerAccountName { get; set; }
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
            var conMod = this.Abilities.FirstOrDefault(x => x.Name == "Constitution");
            if (conMod != null)
            {
                // Need to find some way to add in other random modifiers.
                save += conMod.GetModifier();
            }
            return save;
        }

        public int GetReflexBaseSave()
        {
            int save = 0;
            foreach (RpgClass charClass in Classes)
            {
                save += charClass.ReflexBaseSave;
            }
            var dexMod = this.Abilities.FirstOrDefault(x => x.Name == "Dexterity");
            if (dexMod != null)
            {
                save += dexMod.GetModifier();
            }
            return save;
        }

        public int GetWillBaseSave()
        {
            int save = 0;

            foreach (RpgClass charClass in Classes)
            {
                save += charClass.WillBaseSave;
            }
            var wisMod = this.Abilities.FirstOrDefault(x => x.Name == "Wisdom");
            if (wisMod != null)
            {
                save += wisMod.GetModifier();
            }
            return save;
        }

        public int GetCMB()
        {
            if (this.GetBaseAttackBonus().Any())
            {
                var strMod = this.Abilities.FirstOrDefault(x => x.Name == "Strength");
                if (strMod != null)
                {
                    return this.GetBaseAttackBonus().First() + strMod.GetModifier() + SizeModifier;
                }
                return this.GetBaseAttackBonus().First() + SizeModifier;
            }
            return 0;
        }

        public int GetCMD()
        {
            if (this.GetBaseAttackBonus().Any())
            {
                var baseAttack = this.GetBaseAttackBonus().First() + 10;
                var strMod = this.Abilities.FirstOrDefault(x => x.Name == "Strength");
                if (strMod != null)
                {
                    baseAttack += strMod.GetModifier();
                } 
                var dexMod = this.Abilities.FirstOrDefault(x => x.Name == "Strength");
                if (dexMod != null)
                {
                    baseAttack += dexMod.GetModifier();
                }
                return baseAttack; 
            }
            return 0;
        }
        #endregion

        #region Constructors
        public Character()
        {
            Abilities = new List<Ability>();
        }

        #endregion
    }
}