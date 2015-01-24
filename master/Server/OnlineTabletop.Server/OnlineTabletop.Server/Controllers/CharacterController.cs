using OnlineTabletop.DTOs;
using OnlineTabletop.Mappers;
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
        ICharacterRepository<Character> _characterRepository;

        // GET: api/characters
        // Get the characters belonging to a certain player.
        [Route("player/{playerId}/characters")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<BasicCharacterDTO>))]
        [EnableCors("*", "*", "GET")]
        public IEnumerable<BasicCharacterDTO> Get(string playerId)
        {
            List<BasicCharacterDTO> characterDTOs = new List<BasicCharacterDTO>();
            var characters = _characterRepository.GetCharactersByPlayerId(playerId);

            if (characters != null)
            {
                foreach (var character in characters)
                {
                    var basicDTO = CharacterMapper.BasicDTOFromCharacter(character);
                    characterDTOs.Add(basicDTO);
                }
            }
            return characterDTOs;
        }

        // GET: api/playerId/characterId/5
        [Route("player/{playerId}/characters/{characterId}")]
        public FullCharacterDTO Get(string playerId, string characterId)
        {
            var resp = new HttpResponseMessage();
            try
            {
                var character = _characterRepository.Get(characterId);
                if (character == null)
                {
                    resp.StatusCode = HttpStatusCode.NotFound;
                    resp.Content = new StringContent(string.Format("Character not found with Id = {0}", characterId));
                    resp.ReasonPhrase = "Character Not Found";
                    throw new HttpResponseException(resp);
                }
                if (character.PlayerId != playerId)
                {
                    resp.StatusCode = HttpStatusCode.Conflict;
                    resp.Content = new StringContent(string.Format("Character found but is not associated with the given player Id"));
                    resp.ReasonPhrase = "Mismatch character Id and player Id";
                    throw new HttpResponseException(resp);
                }
                var characterDTO = CharacterMapper.FullDTOFromCharacter(character);
                return characterDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: player/playerId/Character
        [Route("player/{playerId}/character")]
        public IHttpActionResult Post([FromBody]FullCharacterDTO fullCharacterDTO, string playerId)
        {
            if (ModelState.IsValid)
            {
                var character = CharacterMapper.CharacterFromFullDTO(fullCharacterDTO);
                if (!_characterRepository.Contains(character))
                {
                    character.PlayerId = playerId;
                    _characterRepository.Add(character);
                    var basicDTO = CharacterMapper.BasicDTOFromCharacter(character);
                    return Ok(basicDTO);
                }
                else
                {
                    return Conflict();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: player/playerId/character/characterId
        [Route("player/{playerId}/character/{characterId}")]
        public void Put(string playerId, string characterId,[FromBody]FullCharacterDTO fullCharacterDTO)
        {

        }

        // DELETE: api/Character/5
        public void Delete(int id)
        {
        }

        public CharacterController(ICharacterRepository<Character> characterRepository)
        {
            this._characterRepository = characterRepository;
        }
    }
}
