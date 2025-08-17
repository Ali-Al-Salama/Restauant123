using Restaurant.DTO;
using Restaurant.Persistence.Entity;

namespace Restaurant.Services
{
    public interface IMealService
    {
        RestaurantMealResponseForUser GetMeal(long id);
    }
}