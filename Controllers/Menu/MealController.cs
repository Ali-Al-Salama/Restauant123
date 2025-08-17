using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Persistence.Entity;
using Restaurant.Services;
namespace Restaurant.Controller
{
    [ApiController]
    [Route("api/meal")]
    public class MealController:ControllerBase
    {
        private readonly MealService mealServices;
        public MealController(MealService mealServices1)
        {
            mealServices = mealServices1;
        }
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<RestaurantMealResponseForUser> GetMeal(long id)
        {
            if(mealServices.GetMeal(id)!=null)
                return mealServices.GetMeal(id);
            return NotFound();
        }
    }
}