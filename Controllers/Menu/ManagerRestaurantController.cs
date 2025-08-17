using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Persistence.Entity;
using Restaurant.Services;

namespace Restaurant.Controller
{
    [ApiController]
    [Route("api/manager/restaurant")]
    public class ManagerRestaurantController:ControllerBase
    {
        private readonly ManagerRestaurantServices managerRestaurantServices;
        public ManagerRestaurantController(ManagerRestaurantServices managerRestaurantServices1)
        {
            managerRestaurantServices = managerRestaurantServices1;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Get(){
            List<RestaurantMenuResponseForManager> list = managerRestaurantServices.ManagerGet();
            if(list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Get(long id)
        {
            Item item = managerRestaurantServices.ManagerGet(id);
            if(item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Create(RestaurantMenuRequest restaurantMenuRequest)
        {
            Item item = managerRestaurantServices.ManagerCreate(restaurantMenuRequest);
            return Created("Created",item);
        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Update(RestaurantManagerUpdate item)
        {
            bool ok = managerRestaurantServices.ManagerUpdate(item);
            if(!ok)
                return BadRequest();
            return Accepted(item);
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete(long id)
        {
            bool ok = managerRestaurantServices.ManagerDelete(id);
            if(!ok)
                return BadRequest("There are orders related to this item");
            return Accepted();
        }
    }
}