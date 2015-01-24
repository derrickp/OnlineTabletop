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
using System.Web.Http.Results;

namespace OnlineTabletop.Server.Controllers
{
    public class PlayerController : ApiController
    {
        IPlayerRepository<Player> _playerRepository { get; set; }

        // GET: api/Player/5
        [Route("playerinfo/{playerId}")]
        [HttpGet]
        [ResponseType(typeof(BasicPlayerDTO))]
        [EnableCors("*", "*", "GET")]
        public IHttpActionResult Get(string playerId)
        {
            
            Player player = _playerRepository.Get(playerId);
            if (player != null)
            {
                BasicPlayerDTO playerDto = new BasicPlayerDTO(){
                    id = player._id,
                    name = player.name,
                    email = player.email
                };
                return Ok(playerDto);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: api/Player
        [HttpPost]
        [Route("player")]
        public IHttpActionResult Post([FromBody]BasicPlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                var player = _playerRepository.GetByEmail(playerDTO.email);
                if (player != null)
                {
                    return Conflict();
                }
                player = new Player()
                {
                    name = playerDTO.name,
                    email = playerDTO.email
                };
                try
                {
                    _playerRepository.Add(player);
                    player = _playerRepository.GetByEmail(player.email);
                    if (player != null)
                    {
                        return Ok(new BasicPlayerDTO()
                        {
                            id = player._id,
                            name = player.name,
                            email = player.email
                        });
                    }
                    // For some reason the player got added to the repository fine, but we were unable to get the player back from the repository.
                    return Ok();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Player/5
        public void Put(string id, [FromBody]BasicPlayerDTO playerDTO)
        {
        }

        // DELETE: api/Player/5
        public void Delete(string id)
        {
        }

        public PlayerController(IPlayerRepository<Player> playerRepository)
        {
            this._playerRepository = playerRepository;
        }
    }
}
