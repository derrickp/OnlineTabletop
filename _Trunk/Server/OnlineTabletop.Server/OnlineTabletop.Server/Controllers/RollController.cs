using OnlineTabletop.DTOs;
using OnlineTabletop.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTabletop.Server.Controllers
{
    public class RollController : ApiController
    {
        // GET: api/Roll
        [Route("roll/{diceType}")]
        public RollsDTO Get(string diceType)
        {
            return Get(diceType, 1);
        }

        [Route("roll/{diceType}/{numDice}")]
        public RollsDTO Get(string diceType, int numDice)
        {
            var rolls = new RollsDTO();
            switch (diceType)
            {
                case "d4":
                    rolls.diceRolls["d4"] = Roll.d4(numDice);
                    break;
                case "d6":
                    rolls.diceRolls["d6"] = Roll.d6(numDice);
                    break;
                case "d10":
                    rolls.diceRolls["d10"] = Roll.d10(numDice);
                    break;
                case "d12":
                    rolls.diceRolls["d12"] = Roll.d12(numDice);
                    break;
                case "d20":
                    rolls.diceRolls["d20"] = Roll.d20(numDice);
                    break;
                case "d100":
                    rolls.diceRolls["d100"] = Roll.d100(numDice);
                    break;
                default:
                    rolls.diceRolls["d4"] = Roll.d4(numDice);
                    rolls.diceRolls["d6"] = Roll.d6(numDice);
                    rolls.diceRolls["d10"] = Roll.d10(numDice);
                    rolls.diceRolls["d12"] = Roll.d12(numDice);
                    rolls.diceRolls["d20"] = Roll.d20(numDice);
                    rolls.diceRolls["d100"] = Roll.d100(numDice);
                    break;
            }
            return rolls;
        }
    }
}
