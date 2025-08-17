using Restaurant.DTO;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
namespace Restaurant.Services
{
    public class MealService:IMealService
    {
        private readonly AppDBContext appDBContext;
        public MealService(AppDBContext appDBContext1)
        {
            appDBContext = appDBContext1;
        }
        public RestaurantMealResponseForUser GetMeal(long id)
        {
            RestaurantMealResponseForUser? Meal = (from meal in appDBContext.Items where meal.Id == id select new RestaurantMealResponseForUser(
                meal.Id,
                meal.Name,
                meal.URL,
                meal.Category,
                meal.Price,
                meal.Description
            )).FirstOrDefault();
            return Meal;
        }
    }
}