using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using Restaurant.utils;

namespace Restaurant.Controller{
    [ApiController]
    [Route("/api/user")]
    public class LoginController:ControllerBase{
        private readonly LoginService loginService;
        private readonly LoginMessageResponse loginMessageResponse;
        public LoginController(LoginService loginService1,LoginMessageResponse loginMessageResponse1){
            loginService = loginService1;
            loginMessageResponse = loginMessageResponse1;
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult Login([FromQuery]string email,[FromQuery]string password){
            LoginResponse user = loginService.UserLogin(new LoginRequest(email,password));
            if(user==null)
            {
                return NotFound("User Not Found");
            }
            if(user.Message != null){
                if(user.Message == loginMessageResponse.WrongPassword())
                {
                    return BadRequest("Password is wrong");
                }
                if(user.Message == loginMessageResponse.AccessTokenUnActive())
                {
                    return Forbid("Access token is not activated");
                }
            }
            return Ok(user);
        }
    }
}