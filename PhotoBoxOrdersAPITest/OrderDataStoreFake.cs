using PhotoBoxOrdersAPI.DataStore;
using PhotoBoxOrdersAPI.Models;

namespace PhotoBoxOrdersAPITest
{
    public class OrderDataStoreFake : IOrderDataStore
    {
        private readonly List<OrderDto> _orders = new List<OrderDto>();

        public OrderDataStoreFake()
        {
            _orders = new List<OrderDto>()
            {
                new OrderDto()
                {
                    OrderId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), RequiredBinWidth = 133,
                    Items = new List<OrderItemDto>()
                    {
                        new OrderItemDto() { ProductType = ProductType.PhotoBook, Quantity = 1 },
                        new OrderItemDto() { ProductType = ProductType.Calendar, Quantity = 2 },
                        new OrderItemDto() { ProductType = ProductType.Mug, Quantity = 2 }
                    }
                }
            };
        }

        public void AddOrder(OrderDto newOrder)
        {
            _orders.Add(newOrder);
        }

        public OrderDto? GetOrderById(Guid orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }
    }
}
