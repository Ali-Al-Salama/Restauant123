using Restaurant.DTO;
namespace Restaurant.Services
{
    public interface IWeeklyMenu
    {
        List<WeeklyMenuResponse> GetWeeklyMenu();
    }
}