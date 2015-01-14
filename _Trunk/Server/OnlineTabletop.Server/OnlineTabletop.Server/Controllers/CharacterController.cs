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
                name = "Derrick",
                email = "derrickplotsky@gmail.com",
                characters = new List<Character> {
                    new Character() {
                        _id = "TestChar",
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

        // GET: api/characters
        // Get the characters belonging to a certain player.
        [Route("player/{playerId}/characters")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<BasicCharacterDTO>))]
        [EnableCors("*", "*", "GET")]
        public IEnumerable<BasicCharacterDTO> Get(string playerId)
        {
            List<BasicCharacterDTO> characterDTOs = new List<BasicCharacterDTO>();

            Player player = players.FirstOrDefault();
            if (player != null)
            {
                foreach (Character character in player.characters)
                {
                    BasicCharacterDTO basicChar = new BasicCharacterDTO()
                    {
                        name = character.Name,
                        playerName = player.name,
                        characterLevel = character.CharacterLevel(),
                        race = "elf"
                    };
                    basicChar.classes = new List<BasicRPGClassDTO>();
                    foreach (RPGClass rpgClass in character.Classes)
                    {
                        basicChar.classes.Add(new BasicRPGClassDTO
                        {
                            name = rpgClass.Name,
                            level = rpgClass.Level
                        });
                    }
                    characterDTOs.Add(basicChar);
                }
            }
            return characterDTOs;
        }

        // GET: api/playerId/characterId/5
        [Route("player/{playerId}/characters/{detaillevel}/{characterId}")]
        public BasicCharacterDTO Get(string playerId, string characterId, string detaillevel)
        {
            Player player = players.FirstOrDefault();

            var resp = new HttpResponseMessage();

            if (player != null)
            {
                Character character = player.characters.FirstOrDefault(x => x._id == characterId);
                if (character != null)
                {
                    if (detaillevel == "basic")
                    {
                        BasicCharacterDTO basicChar = new BasicCharacterDTO()
                        {
                            name = character.Name,
                            playerName = player.name,
                            characterLevel = character.CharacterLevel(),
                            race = "elf"
                        };
                        basicChar.classes = new List<BasicRPGClassDTO>();
                        foreach (RPGClass rpgClass in character.Classes)
                        {
                            basicChar.classes.Add(new BasicRPGClassDTO
                            {
                                name = rpgClass.Name,
                                level = rpgClass.Level
                            });
                        }
                        return basicChar;
                    }
                    resp.StatusCode = HttpStatusCode.NotImplemented;
                    resp.Content = new StringContent("Level of detail not implemented.");
                    resp.ReasonPhrase = "Level Of Detail Not Implemented";
                    throw new HttpResponseException(resp);
                }
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.Content = new StringContent(string.Format("Character not found with Id = {0}", characterId));
                resp.ReasonPhrase = "Character Not Found";
                throw new HttpResponseException(resp);
            }
            resp.StatusCode = HttpStatusCode.NotFound;
            resp.Content = new StringContent(string.Format("No player with ID = {0}", playerId));
            resp.ReasonPhrase = "Player Not Found";
            throw new HttpResponseException(resp);
        }

        // POST: api/Character
        [Route("player/{playerId}/character")]
        public void Post([FromBody]BasicCharacterDTO basicCharacter, string playerId)
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
