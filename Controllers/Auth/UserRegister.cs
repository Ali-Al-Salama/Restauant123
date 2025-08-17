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
    [Route("api/user/Register")]
    public class UserRegisterController: ControllerBase
    {
        private readonly user_Register _userregisterservices;
        public UserRegisterController(user_Register userregisterservices){
            _userregisterservices=userregisterservices;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult create_User(user_Rigester user){
            var User = _userregisterservices.create_User(user);
            if(User.Message is not null)
                return BadRequest(User.Message);
            return Created("Created",User);
        }

        [HttpGet("get/{id}")]
        public ActionResult get_User(long id)
        {
            List<User> user = _userregisterservices.get_User(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("update")]
        public ActionResult update_User(UserUpdateRequest user)
        {
            user_Response user_Response = _userregisterservices.update_User(user);
            return Ok(user_Response);
        }

        [HttpDelete("delete/{id}")]

        public ActionResult delete_User(long id,[FromQuery]string Password)
        {
            _userregisterservices.delete_User(id,Password);
            return Ok();
        }
    }
}