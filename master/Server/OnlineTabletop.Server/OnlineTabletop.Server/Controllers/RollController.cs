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
                    rolls.diceRolls["d4"] = new List<int>();
                    for (int i = 0; i < numDice; i++)
                    {
                        rolls.diceRolls["d4"].Add(Roll.d4());
                    }
                    break;
                case "d6":
                    rolls.diceRolls["d6"] = new List<int>();
                    for (int i = 0; i < numDice; i++)
                    {
                        rolls.diceRolls["d6"].Add(Roll.d6());
                    }
                    break;
                case "d10":
                    rolls.diceRolls["d10"] = new List<int>();
                    for (int i = 0; i < numDice; i++)
                    {
                        rolls.diceRolls["d10"].Add(Roll.d10());
                    }
                    break;
                case "d12":
                    rolls.diceRolls["d12"] = new List<int>();
                    for (int i = 0; i < numDice; i++)
                    {
                        rolls.diceRolls["d12"].Add(Roll.d12());
                    }
                    break;
                case "d20":
                    rolls.diceRolls["d20"] = new List<int>();
                    for (int i = 0; i < numDice; i++)
                    {
                        rolls.diceRolls["d20"].Add(Roll.d20());
                    }
                    break;
                case "d100":
                    rolls.diceRolls["d100"] = new List<int>();
                    for (int i = 0; i < numDice; i++)
                    {
                        rolls.diceRolls["d100"].Add(Roll.d100());
                    }
                    break;
                default:
                    rolls.diceRolls["d4"] = new List<int>();
                    rolls.diceRolls["d6"] = new List<int>();
                    rolls.diceRolls["d8"] = new List<int>();
                    rolls.diceRolls["d10"] = new List<int>();
                    rolls.diceRolls["d12"] = new List<int>();
                    rolls.diceRolls["d20"] = new List<int>();
                    rolls.diceRolls["d100"] = new List<int>();
                    for (int i = 0; i < numDice; i++)
                    {
                        rolls.diceRolls["d4"].Add(Roll.d4());
                        rolls.diceRolls["d6"].Add(Roll.d6());
                        rolls.diceRolls["d8"].Add(Roll.d8());
                        rolls.diceRolls["d10"].Add(Roll.d10());
                        rolls.diceRolls["d12"].Add(Roll.d12());
                        rolls.diceRolls["d20"].Add(Roll.d20());
                        rolls.diceRolls["d100"].Add(Roll.d100());
                    }
                    break;
            }
            return rolls;
        }
    }
}
