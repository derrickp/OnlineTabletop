using OnlineTabletop.DTOs;
using OnlineTabletop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Mappers
{
    public static class AbilityMapper
    {
        static AbilityMapper()
        { }

        public static Ability GetAbilityFromDTO(AbilityDTO abilityDTO)
        {
            var ability = new Ability();

            switch (abilityDTO.name)
            {
                case "strength":
                    ability.Name = AbilityNames.Strength;
                    break;
                case "dexterity":
                    ability.Name = AbilityNames.Dexterity;
                    break;
                case "constitution":
                    ability.Name = AbilityNames.Constitution;
                    break;
                case "wisdom":
                    ability.Name = AbilityNames.Wisdom;
                    break;
                case "intelligence":
                    ability.Name = AbilityNames.Intelligence;
                    break;
                case "charisma":
                    ability.Name = AbilityNames.Charisma;
                    break;
                default:
                    break;
            }
            ability.Score = abilityDTO.score;
            ability.TempAdjustment = abilityDTO.tempAdjustment;

            return ability;
        }

        public static AbilityDTO GetDTOFromAbility(Ability ability)
        {
            var abilityDTO = new AbilityDTO();
            
            switch(ability.Name)
            {
                case AbilityNames.Strength:
                    abilityDTO.name = "strength";
                    break;
                case AbilityNames.Dexterity:
                    abilityDTO.name = "dexterity";
                    break;
                case AbilityNames.Constitution:
                    abilityDTO.name = "constitution";
                    break;
                case AbilityNames.Wisdom:
                    abilityDTO.name = "wisdom";
                    break;
                case AbilityNames.Intelligence:
                    abilityDTO.name = "intelligence";
                    break;
                case AbilityNames.Charisma:
                    abilityDTO.name = "charisma";
                    break;
                default:
                    break;
            }
            abilityDTO.score = ability.Score;
            abilityDTO.tempAdjustment = ability.TempAdjustment;
            abilityDTO.modifier = ability.GetModifier();

            return abilityDTO;
        }
    }
}
