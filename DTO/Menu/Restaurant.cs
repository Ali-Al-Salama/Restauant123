namespace Restaurant.DTO
{
    public record RestaurantMenuRequest(
        string Name,
        string URL,
        int Category,
        float Price,
        string Description,
        bool IsAvailable
    );
    public record RestaurantMenuResponseForUser(
        long Id,
        string Name,
        string URL,
        string Category,
        string Description,
        float Price
    );
    public record RestaurantMealResponseForUser(
        long Id,
        string Name,
        string URL,
        string Category,
        float Price,
        string Description
    );
    public record RestaurantManagerUpdate(
        long Id,
        string Name,
        string URL,
        int Category,
        float Price,
        string Description,
        bool IsAvailable
    );
    public record RestaurantMenuResponseForManager(
        long Id,
        string Name,
        string URL,
        string Category,
        float Price,
        string Description,
        bool IsAvailable
    );
}