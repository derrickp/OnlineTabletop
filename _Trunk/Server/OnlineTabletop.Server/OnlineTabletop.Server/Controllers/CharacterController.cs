using OnlineTabletop.DTOs;
using OnlineTabletop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace OnlineTabletop.Server.Controllers
{
    public class CharacterController : ApiController
    {
        public static List<Player> players = new List<Player>
        {
            new Player(){
                Name = "Derrick",
                Email = "derrickplotsky@gmail.com",
                Characters = new List<Character> {
                    new Character() {
                        Name = "RazzleMan",
                        Classes = new List<RPGClass> {
                            new RPGClass() {
                                Name = "Barbarian",
                                Level = 1
                            }
                        }
                    }
                }
            }
        };


        // GET: api/Character
        [Route("characters/{playerId}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<BasicCharacterDTO>))]
        [EnableCors("*", "*", "GET")]
        public IEnumerable<BasicCharacterDTO> Get(string playerId)
        {
            List<BasicCharacterDTO> characterDTOs = new List<BasicCharacterDTO>();

            Player player = players.FirstOrDefault();
            if (player != null)
            {
                foreach (Character character in player.Characters)
                {
                    BasicCharacterDTO basicChar = new BasicCharacterDTO()
                    {
                        name = character.Name,
                        playerName = player.Name,
                        characterLevel = character.CharacterLevel(),
                        race = "elf"
                    };
                    basicChar.classes = new Dictionary<string, int>();
                    foreach (RPGClass rpgClass in character.Classes)
                    {
                        basicChar.classes[rpgClass.Name] = rpgClass.Level;
                    }
                    characterDTOs.Add(basicChar);
                }
                return characterDTOs;
            }
            return characterDTOs;
        }

        // GET: api/Character/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Character
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Character/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Character/5
        public void Delete(int id)
        {
        }
    }
}
