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
    public class PlayerController : ApiController
    {
        public static List<Player> players = new List<Player>
        {
            new Player(){
                name = "Derrick",
                email = "derrickplotsky@gmail.com"
            }
        };

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
                Player player = new Player()
                {
                    name = playerDTO.name,
                    email = playerDTO.email
                };
                _playerRepository.Add(player);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Player/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
        }

        public PlayerController(IPlayerRepository<Player> playerRepository)
        {
            this._playerRepository = playerRepository;
        }
    }
}
