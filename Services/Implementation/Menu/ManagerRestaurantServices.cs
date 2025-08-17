using Microsoft.EntityFrameworkCore;
using Restaurant.DTO;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
namespace Restaurant.Services
{
    public class ManagerRestaurantServices:IManagerServices
    {
        private readonly AppDBContext appDBContext;
        private readonly Category category;
        private readonly UploadPicture uploadPicture;
        public ManagerRestaurantServices(AppDBContext appDBContext1,Category category1,UploadPicture uploadPicture1)
        {
            appDBContext = appDBContext1;
            category = category1;
            uploadPicture = uploadPicture1;
        }
        public List<RestaurantMenuResponseForManager> ManagerGet()
        {
            List<RestaurantMenuResponseForManager> restaurantMenu = appDBContext.Items.Select
            (
                menu => new RestaurantMenuResponseForManager(
                    menu.Id,
                    menu.Name,
                    menu.URL,
                    menu.Category,
                    menu.Price,
                    menu.Description,
                    menu.IsAvailable
                )
            ).ToList();
            return restaurantMenu;
        }
        public Item ManagerGet(long id)
        {
            Item? Meal = (from meal in appDBContext.Items where meal.Id == id select meal).FirstOrDefault();
            return Meal;
        }
        public void ManagerCreate(){ }
        public Item ManagerCreate(RestaurantMenuRequest restaurantMenuRequest)
        {
            Item item = new Item(
                restaurantMenuRequest.Name,
                category.Categories(restaurantMenuRequest.Category),
                uploadPicture.UploadPictureFunction(restaurantMenuRequest.URL),
                restaurantMenuRequest.Price,
                restaurantMenuRequest.Description,
                restaurantMenuRequest.IsAvailable
            );
            appDBContext.Items.Add(item);
            appDBContext.SaveChanges();
            return item;
        }
        public void ManagerUpdate(){ }
        public bool ManagerUpdate(RestaurantManagerUpdate item)
        {
            Item? item2 = appDBContext.Items.Find(item.Id);
            if(item2 == null)
                return false;
            item2.Name=item.Name;
            item2.Category=category.Categories(item.Category);
            item2.URL=uploadPicture.UploadPictureFunction(item.URL);
            item2.Price=item.Price;
            item2.Description=item.Description;
            item2.IsAvailable=item.IsAvailable;
            appDBContext.Items.Entry(item2).State = EntityState.Modified;
            appDBContext.SaveChanges();
            return true;
        }
        public void ManagerDelete(){ }
        public bool ManagerDelete(long id)
        {
            Item? item = appDBContext.Items.Find(id);
            Order? orders = (from orderr in appDBContext.Order where orderr.ItemId == id select orderr).FirstOrDefault(); 
            if(item == null||orders!=null)
                return false;
            appDBContext.Items.Remove(item);
            appDBContext.SaveChanges();
            return true;
        }
    }
}