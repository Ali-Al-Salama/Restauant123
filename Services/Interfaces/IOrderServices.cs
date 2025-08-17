using Restaurant.DTO;
using Restaurant.Persistence.Entity;
namespace Restaurant.Services
{
    public interface IOrderServices
    {
        OrderResponse CreateOrder();

        List<Order> GetOrders();

        OrderResponse UpdateOrder();

        OrderResponse DeleteOrder();
    }
}