
using Restaurant.DTO;

namespace Restaurant.Services
{
    public interface IRestaurantService
    {
        List<RestaurantMenuResponseForUser> GetRestaurantMenu();
    }
}