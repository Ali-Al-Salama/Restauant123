
using Restaurant.Persistence.Entity;

namespace Restaurant.DTO
{
    public record WeeklyMenuResponse(
        Item Details,
        DateTime Time
    );
    public record WeeklyMenuRequest(
        long Id,
        long ItemId,
        DateTime Time 
    );
    public record WeeklyMenuRequestForCreate(
        long ItemId,
        DateTime Time 
    );
}