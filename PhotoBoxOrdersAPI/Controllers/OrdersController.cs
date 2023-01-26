using Microsoft.AspNetCore.Mvc;
using PhotoBoxOrdersAPI.DataStore;
using PhotoBoxOrdersAPI.Helpers;
using PhotoBoxOrdersAPI.Models;

namespace PhotoBoxOrdersAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderDataStore _dataStore;
        public OrdersController(IOrderDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpPost(Name = "CreateOrder")]
        public ActionResult<decimal> Post(OrderDto newOrder)
        {
            // Validate the new order payload
            if (newOrder.Items == null || newOrder.Items.Count == 0)
                return BadRequest(new { error = "Order contains no items" });

            if (newOrder.Items.Any(i => i.Quantity < 1))
                return BadRequest(new { error = "All items must have a quantity of at least 1" });

            // Perform any calculations to the order
            newOrder.RequiredBinWidth = ProductHelper.CalculateMinimumWidth(newOrder.Items);

            // Persist into the data store
            _dataStore.AddOrder(newOrder);

            return newOrder.RequiredBinWidth;
        }

        [HttpGet(Name = "GetOrderDetails")]
        public ActionResult<OrderDto> Get(Guid orderId)
        {
            var foundOrder = _dataStore.GetOrderById(orderId);

            if (foundOrder == null)
                return NotFound();

            return foundOrder;
        }
    }
}