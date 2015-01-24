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
                    ability.Name = "Strength";
                    break;
                case "dexterity":
                    ability.Name = "Dexterity";
                    break;
                case "constitution":
                    ability.Name = "Constitution";
                    break;
                case "wisdom":
                    ability.Name = "Wisdom";
                    break;
                case "intelligence":
                    ability.Name = "Intelligence";
                    break;
                case "charisma":
                    ability.Name = "Charisma";
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
                case "Strength":
                    abilityDTO.name = "strength";
                    break;
                case "Dexterity":
                    abilityDTO.name = "dexterity";
                    break;
                case "Constitution":
                    abilityDTO.name = "constitution";
                    break;
                case "Wisdom":
                    abilityDTO.name = "wisdom";
                    break;
                case "Intelligence":
                    abilityDTO.name = "intelligence";
                    break;
                case "Charisma":
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
