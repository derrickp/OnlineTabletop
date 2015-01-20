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

        // GET: api/characters
        // Get the characters belonging to a certain player.
        [Route("player/{playerId}/characters")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<BasicCharacterDTO>))]
        [EnableCors("*", "*", "GET")]
        public IEnumerable<BasicCharacterDTO> Get(string playerId)
        {
            List<BasicCharacterDTO> characterDTOs = new List<BasicCharacterDTO>();

            Player player = new Player();
            if (player != null)
            {
                foreach (string characterId in player.characterIds)
                {
                    var character = new Character();
                    BasicCharacterDTO basicChar = new BasicCharacterDTO()
                    {
                        name = character.Name,
                        playerName = player.name,
                        characterLevel = character.GetCharacterLevel(),
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
        [Route("player/{playerId}/characters/{characterId}")]
        public FullCharacterDTO Get(string playerId, string characterId)
        {
            Player player = new Player();

            var resp = new HttpResponseMessage();

            if (player != null)
            {
                var character = new Character();
                if (character != null)
                {
                    //if (detaillevel == "basic")
                    {
                        FullCharacterDTO fullCharacterDTO = new FullCharacterDTO()
                        {
                            name = "Crazy Name",
                            playerName = "Derrick",
                            characterLevel = 1,
                            race = "elf"
                        };
                        fullCharacterDTO.classes = new List<BasicRPGClassDTO>();
                        foreach (RPGClass rpgClass in character.Classes)
                        {
                            fullCharacterDTO.classes.Add(new BasicRPGClassDTO
                            {
                                name = rpgClass.Name,
                                level = rpgClass.Level
                            });
                        }
                        return fullCharacterDTO;
                    }
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
        public IHttpActionResult Post([FromBody]FullCharacterDTO basicCharacter, string playerId)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        // PUT: api/Character/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Character/5
        public void Delete(int id)
        {
        }

        public CharacterController(IPlayerRepository<Player> playerRepository)
        {
            //this._playerRepository = playerRepository;
        }
    }
}
