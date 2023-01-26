using PhotoBoxOrdersAPI.Controllers;
using PhotoBoxOrdersAPI.DataStore;
using PhotoBoxOrdersAPI.Models;

namespace PhotoBoxOrdersAPITest
{
    [TestClass]
    public class OrderDataStoreTests
    {
        private readonly OrdersController _controller;
        private readonly IOrderDataStore _dataStore;

        public OrderDataStoreTests()
        {
            _dataStore = new OrderDataStoreFake();
            _controller = new OrdersController(_dataStore);
        }

        [TestMethod]
        public void GetOrderById_Found()
        {
            var foundOrder = _controller.Get(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            Assert.IsNotNull(foundOrder);
        }

        [TestMethod]
        public void GetOrderById_NotFound()
        {
            var foundOrder = _controller.Get(new Guid("1fa85f64-5717-4562-b3fc-2c963f66afa6"));

            Assert.IsNotNull(foundOrder);
        }

        [TestMethod]
        public void AddOrder_MinimumCorrect()
        {
            var result = _controller.Post(new OrderDto()
            {
                OrderId = new Guid("B074A95C-845F-4E6C-BDDD-8E1B7A92FBB9"),
                Items = new List<OrderItemDto>()
                {
                    new OrderItemDto() { ProductType = ProductType.Mug, Quantity = 3 },
                    new OrderItemDto() { ProductType = ProductType.Canvas, Quantity = 1 }
                }
            });

            Assert.AreEqual(110, result.Value);
        }

        [TestMethod]
        public void AddOrder_MinimumCorrect_FetchBack()
        {
            var result = _controller.Post(new OrderDto()
            {
                OrderId = new Guid("FCA5FA0F-41B8-469E-805D-307228E873D8"),
                Items = new List<OrderItemDto>()
                {
                    new OrderItemDto() { ProductType = ProductType.Mug, Quantity = 6 },
                    new OrderItemDto() { ProductType = ProductType.Calendar, Quantity = 2 },
                    new OrderItemDto() { ProductType = ProductType.Card, Quantity = 14 }
                }
            });

            Assert.AreEqual(273.8M, result.Value);

            var foundOrder = _controller.Get(new Guid("FCA5FA0F-41B8-469E-805D-307228E873D8"));

            Assert.IsNotNull(foundOrder);
        }
    }
}
