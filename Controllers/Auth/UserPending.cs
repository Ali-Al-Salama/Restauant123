using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Persistence.Entity;
using Restaurant.Services.Implementation;
using Restaurant.DTO;
using static restaurant.DTO.User.user;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/user/Pending-register")]
    public class UserPendingController : ControllerBase
    {
        private readonly user_Pending userpendingservices;

        public UserPendingController(user_Pending userpendingservices1){
            userpendingservices=userpendingservices1;

        }
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult create_User_Pending(pending_User_Request user){
            pending_User_Responce pending_User_Responce = userpendingservices.create_User(user);
            if(pending_User_Responce.Message != null)
                return StatusCode(500,"Internal Server Error");
            return Created("Created",user);
        }

     /*   [HttpGet("get/{id}")]

        public ActionResult GetUserPending(long id){
             //userpendingservices.getPendingUser(id);
             List<PendingUser> list = userpendingservices.getPendingUser(id);
            if(list == null)
                return NotFound();
            
            return Ok(list);

        }
        */
    }
}