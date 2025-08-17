using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.Implementation;
using Restaurant.DTO;
using Restaurant.Persistence.Entity;
using static restaurant.DTO.User.user;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/manager/user")]
    public class UserController:ControllerBase
    {
        private readonly user_Service _userservices;
        public UserController(user_Service userservices)
        {
            _userservices = userservices;
        }
        [HttpPost("create")]
        public ActionResult CreateUser(ManagerUserRequest userRequest)
        {
            user_Response user_Response = _userservices.CreateUser(userRequest);
            if(user_Response.Message is not null)
            {
                return BadRequest(user_Response.Message);
            }
            return Created("Created", user_Response);
        }
        [HttpGet("get-all")]
        public ActionResult GetUser()
        {
            List<User> users = _userservices.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("get/{id}")]
        public ActionResult GetUser(long id)
        {
            List<User> user = _userservices.GetUsers(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("update")]
        public ActionResult UpdateUser(ManagerUserUpdateRequest user)
        {
            user_Response user_Response = _userservices.UpdateUser(user);
            return Ok(user_Response);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteUser(long id,string Password)
        {
            _userservices.DeleteUser(id,Password);
            return Ok();
        }
    }
}