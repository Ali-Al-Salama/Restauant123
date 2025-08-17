using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
namespace Restaurant.Controller
{
    [ApiController]
    [Route("api/user/reset-password")]
    public class ResetPasswordController:ControllerBase
    {
        private readonly ResetPasswordService resetPasswordService;
        public ResetPasswordController(ResetPasswordService resetPasswordService1)
        {
            resetPasswordService = resetPasswordService1;
        }
        [HttpPost("send-confirm-code")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ResetPassword()
        {
            string? UserId = HttpContext.Request.Headers["UserId"];
            bool test = resetPasswordService.SendCode2User(UserId);
            if(test)
            {
                return Accepted();
            }
            return BadRequest();
        }
        [HttpPut("verificate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult ResetPassword([FromBody]ResetRequest resetRequest)
        {
            string? UserId = HttpContext.Request.Headers["UserId"];
            bool test = resetPasswordService.ResetPassword(resetRequest.Code,UserId,resetRequest.NewPassword);
            if(test)
            {
                return Accepted();
            }
            return BadRequest();
        }
    }
}