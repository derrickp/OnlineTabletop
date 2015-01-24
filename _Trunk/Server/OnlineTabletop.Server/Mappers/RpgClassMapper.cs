using OnlineTabletop.DTOs;
using OnlineTabletop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Mappers
{
    public static class RpgClassMapper
    {
        static RpgClassMapper() { }

        public static RpgClass GetRpgClassFromDTO(RpgClassDTO classDTO)
        {
            var rpgClass = new RpgClass()
            {
                Level = classDTO.level,
                HitDice = classDTO.hitDice,
                Name = classDTO.name,
                FortitudeBaseSave = classDTO.fortitudeBaseSave,
                ReflexBaseSave = classDTO.reflexBaseSave,
                WillBaseSave = classDTO.willBaseSave,
                BaseAttacks = classDTO.baseAttacks
            };

            return rpgClass;
        }

        public static RpgClassDTO GetDTOFromClass(RpgClass rpgClass)
        {
            var rpgClassDTO = new RpgClassDTO()
            {
                level = rpgClass.Level,
                hitDice = rpgClass.HitDice,
                name = rpgClass.Name,
                fortitudeBaseSave = rpgClass.FortitudeBaseSave,
                reflexBaseSave = rpgClass.ReflexBaseSave,
                willBaseSave = rpgClass.WillBaseSave,
                baseAttacks = rpgClass.BaseAttacks
            };

            return rpgClassDTO;
        }
    }
}
