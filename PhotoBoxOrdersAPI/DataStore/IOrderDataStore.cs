using PhotoBoxOrdersAPI.Models;

namespace PhotoBoxOrdersAPI.DataStore
{
    public interface IOrderDataStore
    {
        void AddOrder(OrderDto newOrder);
        OrderDto? GetOrderById(Guid orderId);
    }
}
