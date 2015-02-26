using OnlineTabletop.DTOs;
using OnlineTabletop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace OnlineTabletop.Server.Controllers
{
    [RoutePrefix("player")]
    public class PlayerController : ApiController
    {
        IPlayerRepository<Player> _playerRepository { get; set; }

        [Route("info/{playerName}")]
        [HttpGet]
        [ResponseType(typeof(BasicPlayerDTO))]
        public IHttpActionResult Get(string playerName)
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            Player player = _playerRepository.GetByAccountName(playerName);
            if (player != null)
            {
                BasicPlayerDTO playerDto = new BasicPlayerDTO(){
                    _id = player._id,
                    name = player.AccountName,
                    email = player.Email
                };
                return Ok(playerDto);
            }
            else
            {
                return NotFound();
            }
            
        }

        [Route("info")]
        [HttpGet]
        [ResponseType(typeof(FullPlayerDTO))]
        public IHttpActionResult Get()
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            if (principal == null)
            {
                return BadRequest();
                
            }

            var accountNameClaim = principal.Claims.FirstOrDefault(x => x.Type == "userName");
            if (accountNameClaim == null)
            {
                return BadRequest();
            }

            var playerName = accountNameClaim.Value.ToString();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                return BadRequest();
            }

            Player player = _playerRepository.GetByAccountName(playerName);
            if (player != null)
            {
                var playerDto = new FullPlayerDTO()
                {
                    _id = player._id,
                    name = player.AccountName,
                    email = player.Email,
                    joinDate = player.JoinDate,
                    displayName = player.DisplayName
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
                    AccountName = playerDTO.name,
                    Email = playerDTO.email
                };
                try
                {
                    _playerRepository.Add(player);
                    player = _playerRepository.GetByEmail(player.Email);
                    if (player != null)
                    {
                        return Ok(new BasicPlayerDTO()
                        {
                            _id = player._id,
                            name = player.AccountName,
                            email = player.Email
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
