using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;
using Restaurant.utils;
using static Restaurant.DTO.token;

namespace Restaurant.Controller
{
    [ApiController]
    [Route("/api/user")]
    public class AccessTokenController : ControllerBase
    {
        private readonly AccessTokenService accessTokenService;
        private readonly TokenMessageResponse tokenMessageResponse;
        public AccessTokenController
        (
            AccessTokenService accessTokenService1,
            TokenMessageResponse tokenMessageResponse1
        )
        {
            accessTokenService = accessTokenService1;
            tokenMessageResponse = tokenMessageResponse1;
        }
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AccessToken(string refreshtoken)
        {
            AccessResponse accessTokenResponse = accessTokenService.GenerateAccessToken(refreshtoken);
            if(accessTokenResponse.Message == tokenMessageResponse.AccessResponseNotFound())
            {
                return NotFound(tokenMessageResponse.AccessResponseNotFound());
            }
            if(accessTokenResponse.Message == tokenMessageResponse.AccessTokenUnActive())
            {
                return NotFound(tokenMessageResponse.AccessResponseNotFound());
            }
            if(accessTokenResponse.Message == tokenMessageResponse.WrongRefreshToken())
            {
                return BadRequest(tokenMessageResponse.WrongRefreshToken());
            }
            return Ok(accessTokenResponse.AccessToken);
        }
    }
}