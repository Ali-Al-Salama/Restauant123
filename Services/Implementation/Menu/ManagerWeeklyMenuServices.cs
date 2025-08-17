using Microsoft.EntityFrameworkCore;
using Restaurant.DTO;
using Restaurant.Persistence;
using Restaurant.Persistence.Entity;
namespace Restaurant.Services
{
    public class ManagerWeeklyMenuServices:IManagerServices
    {
        private readonly AppDBContext appDBContext;
        public ManagerWeeklyMenuServices(AppDBContext appDBContext1)
        {
            appDBContext = appDBContext1;
        }
        public List<WeeklyMenuResponse> MangerGet()
        {
            var weeklymenu = appDBContext.WeeklyMenu.Select(weekly=>new WeeklyMenuResponse(
                weekly.Item,
                weekly.Date
            )).ToList();
            return weeklymenu;
        }
        public WeeklyMenu MangerCreate(WeeklyMenuRequestForCreate weeklyMenuRequest)
        {
            WeeklyMenu weeklyitem = new WeeklyMenu(
                weeklyMenuRequest.ItemId,
                weeklyMenuRequest.Time
            );
            appDBContext.WeeklyMenu.Add(weeklyitem);
            weeklyitem.Item = appDBContext.Items.Find(weeklyitem.ItemId);
            appDBContext.SaveChanges();
            return weeklyitem;
        }
        public bool MangerUpdate(WeeklyMenuRequest weeklyitem)
        {
            WeeklyMenu? weeklyMenu = appDBContext.WeeklyMenu.Find(weeklyitem.Id);
            Item? item = appDBContext.Items.Find(weeklyitem.ItemId);
            if(weeklyMenu == null||item == null)
                return false;
            weeklyMenu.Date = weeklyitem.Time;
            weeklyMenu.ItemId = weeklyitem.ItemId;
            appDBContext.WeeklyMenu.Entry(weeklyMenu).State = EntityState.Modified;
            appDBContext.SaveChanges();
            return true;
        }
        public bool MangerDelete(long id)
        {
            var weeklyitem = appDBContext.WeeklyMenu.Find(id);
            if(weeklyitem == null)
                return false;
            appDBContext.WeeklyMenu.Remove(weeklyitem);
            appDBContext.SaveChanges();
            return true;
        }
        public void ManagerCreate()
        {
            throw new NotImplementedException();
        }

        public void ManagerUpdate()
        {
            throw new NotImplementedException();
        }

        public void ManagerDelete()
        {
            throw new NotImplementedException();
        }
    }
}