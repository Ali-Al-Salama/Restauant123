using Restaurant.DTO;
using Restaurant.Persistence;
namespace Restaurant.Services{
    public class RestaurantService:IRestaurantService
    {
        private readonly AppDBContext appDBContext;
        public RestaurantService(AppDBContext appDBContext1)
        {
            appDBContext = appDBContext1;
        }

        public List<RestaurantMenuResponseForUser> GetRestaurantMenu()
        {
            List<RestaurantMenuResponseForUser> restaurantMenu = (from items in appDBContext.Items
                                                                  where items.IsAvailable == true
                                                                  select new RestaurantMenuResponseForUser(
                                                                    items.Id,
                                                                    items.Name,
                                                                    items.URL,
                                                                    items.Category,
                                                                    items.Description,
                                                                    items.Price
                                                                    )).ToList();
            return restaurantMenu;
        }
    }
}