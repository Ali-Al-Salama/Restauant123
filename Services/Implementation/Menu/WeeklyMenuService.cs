using System.ComponentModel.Design;
using Restaurant.DTO;
using Restaurant.Persistence;
namespace Restaurant.Services{
    public class WeeklyMenuService:IWeeklyMenu
    {
        private readonly AppDBContext appDBContext;
        public WeeklyMenuService(AppDBContext appDBContext1)
        {
            appDBContext = appDBContext1;
        }
        public List<WeeklyMenuResponse> GetWeeklyMenu()
        {
            List<WeeklyMenuResponse> weeklyMenu = (from menu in appDBContext.WeeklyMenu
                                                    join items in appDBContext.Items
                                                    on menu.ItemId equals items.Id
                                                    where items.IsAvailable
                                                    select new WeeklyMenuResponse(
                                                        items,
                                                        menu.Date
                                                    )).ToList();
            return weeklyMenu;
        }
    }
}