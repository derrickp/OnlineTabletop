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
                Name = "Derrick",
                Email = "derrickplotsky@gmail.com"
            }
        };

        //// GET: api/Player
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Player/5
        [Route("playerinfo/{playerId}")]
        [HttpGet]
        [ResponseType(typeof(BasicPlayerDTO))]
        [EnableCors("*", "*", "GET")]
        public IHttpActionResult Get(string playerId)
        {
            Player player = players.FirstOrDefault();
            if (player != null)
            {
                BasicPlayerDTO playerDto = new BasicPlayerDTO(){
                    id = player.Id,
                    name = player.Name,
                    email = player.Email
                };
                return Ok(playerDto);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: api/Player
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Player/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
        }
    }
}
