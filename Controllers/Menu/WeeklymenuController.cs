using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;

namespace Restaurant.Controller{
    [ApiController]
    [Route("api/weekly")]
    public class WeeklymenuController:ControllerBase
    {
        private readonly WeeklyMenuService weeklyMenuServices;
        public WeeklymenuController(WeeklyMenuService weeklyMenuServices1)
        {
            weeklyMenuServices = weeklyMenuServices1;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetWeeklyMenu()
        {
            List<WeeklyMenuResponse> list = weeklyMenuServices.GetWeeklyMenu();
            if(list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
    }
}