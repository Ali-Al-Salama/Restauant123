using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Persistence.Entity;
using Restaurant.Services;
namespace Restaurant.Controller
{
    [ApiController]
    [Route("api/manager/weekly")]
    public class ManagerWeeklyMenuController:ControllerBase
    {
        private readonly ManagerWeeklyMenuServices mangerWeeklyMenuServices;
        public ManagerWeeklyMenuController(ManagerWeeklyMenuServices mangerWeeklyMenuServices1)
        {
            mangerWeeklyMenuServices = mangerWeeklyMenuServices1;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Get()
        {
            List<WeeklyMenuResponse> list = mangerWeeklyMenuServices.MangerGet();
            if(list == null)
                return NotFound();
            return Ok(list);
        }
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Create(WeeklyMenuRequestForCreate weeklyMenuRequest)
        {
            WeeklyMenu weeklyMenu = mangerWeeklyMenuServices.MangerCreate(weeklyMenuRequest);
            return Created("Created",weeklyMenu);
        }
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Update(WeeklyMenuRequest weeklyMenu)
        {
            bool ok = mangerWeeklyMenuServices.MangerUpdate(weeklyMenu);
            if(!ok)
                return BadRequest();
            return Ok(weeklyMenu);
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Delete([FromRoute]long id)
        {
            bool ok = mangerWeeklyMenuServices.MangerDelete(id);
            if(!ok)
                return BadRequest();
            return Ok();
        }
    }
}