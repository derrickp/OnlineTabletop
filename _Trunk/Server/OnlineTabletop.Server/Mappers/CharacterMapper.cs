using OnlineTabletop.DTOs;
using OnlineTabletop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Mappers
{
    public static class CharacterMapper
    {
        public static Character CharacterFromFullDTO(FullCharacterDTO fullCharacterDTO)
        {
            var character = new Character();
            if (!string.IsNullOrWhiteSpace(fullCharacterDTO.id)) character._id = fullCharacterDTO.id;
            if (!string.IsNullOrWhiteSpace(fullCharacterDTO.playerId)) character.PlayerId = fullCharacterDTO.playerId;
            character.Name = fullCharacterDTO.name;
            character.Race = fullCharacterDTO.race;
            character.Strength = AbilityMapper.GetAbilityFromDTO(fullCharacterDTO.strength);
            character.Dexterity = AbilityMapper.GetAbilityFromDTO(fullCharacterDTO.dexterity);
            character.Wisdom = AbilityMapper.GetAbilityFromDTO(fullCharacterDTO.wisdom);
            character.Constitution = AbilityMapper.GetAbilityFromDTO(fullCharacterDTO.constitution);
            character.Intelligence = AbilityMapper.GetAbilityFromDTO(fullCharacterDTO.intelligence);
            character.Charisma = AbilityMapper.GetAbilityFromDTO(fullCharacterDTO.charisma);

            character.Size = fullCharacterDTO.size;
            character.SizeModifier = fullCharacterDTO.sizeModifier;

            character.Classes = new List<RpgClass>();

            foreach (RpgClassDTO classDTO in fullCharacterDTO.classes)
            {
                var rpgClass = RpgClassMapper.GetRpgClassFromDTO(classDTO);
                character.Classes.Add(rpgClass);
            }

            return character;
        }

        public static FullCharacterDTO FullDTOFromCharacter(Character character)
        {
            var characterDTO = new FullCharacterDTO();
            characterDTO.id = character._id;
            characterDTO.playerId = character.PlayerId;
            characterDTO.name = character.Name;
            characterDTO.race = character.Race;
            characterDTO.strength = AbilityMapper.GetDTOFromAbility(character.Strength);
            characterDTO.dexterity = AbilityMapper.GetDTOFromAbility(character.Dexterity);
            characterDTO.constitution = AbilityMapper.GetDTOFromAbility(character.Constitution);
            characterDTO.wisdom = AbilityMapper.GetDTOFromAbility(character.Wisdom);
            characterDTO.intelligence = AbilityMapper.GetDTOFromAbility(character.Intelligence);
            characterDTO.charisma = AbilityMapper.GetDTOFromAbility(character.Charisma);
            characterDTO.size = character.Size;
            characterDTO.sizeModifier = character.SizeModifier;

            characterDTO.classes = new List<RpgClassDTO>();

            foreach (RpgClass rpgClass in character.Classes)
            {
                var rpgClassDTO = RpgClassMapper.GetDTOFromClass(rpgClass);
                characterDTO.classes.Add(rpgClassDTO);
            }

            return characterDTO;
        }

        public static BasicCharacterDTO BasicDTOFromCharacter(Character character)
        {
            var characterDTO = new BasicCharacterDTO();
            characterDTO.id = character._id;
            characterDTO.playerId = character.PlayerId;
            characterDTO.name = character.Name;
            characterDTO.race = character.Race;

            characterDTO.classes = new List<RpgClassDTO>();

            foreach (RpgClass rpgClass in character.Classes)
            {
                var rpgClassDTO = RpgClassMapper.GetDTOFromClass(rpgClass);
                characterDTO.classes.Add(rpgClassDTO);
            }

            return characterDTO;
        }

        static CharacterMapper()
        { }
    }
}
