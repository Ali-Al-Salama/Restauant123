using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;

namespace Restaurant.Controller{
    [ApiController]
    [Route("api/restaurant/menu")]
    public class RestaurantmenuController:ControllerBase
    {
        private readonly RestaurantService restaurantService;
        public RestaurantmenuController(RestaurantService restaurantService1)
        {
            restaurantService = restaurantService1;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetMenu(){
            List<RestaurantMenuResponseForUser> list = restaurantService.GetRestaurantMenu();
            if(list == null){
                return NotFound();
            }
            return Ok(list);
        }
    }
}