using Dapper;
using PhotoBoxOrdersAPI.Models;
using System.Data.SqlClient;
using System.Transactions;

namespace PhotoBoxOrdersAPI.DataStore
{
    public class OrderDataStore : IOrderDataStore
    {
        private readonly IConfiguration _configuration;

        public OrderDataStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Add a new order into the database with its associated items,
        /// calculating the minimum bin width required
        /// </summary>
        /// <param name="newOrder">Order details DTO</param>
        /// <returns>Minimum bin width</returns>
        public void AddOrder(OrderDto newOrder)
        {
            using (var trans = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.MaximumTimeout
                },
            TransactionScopeAsyncFlowOption.Enabled))
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                // Create the head order record
                conn.Execute(@"INSERT INTO [Order] (OrderId, Created, RequiredBinWidth)
                            VALUES(@OrderId, GETUTCDATE(), @RequiredBinWidth)",
                new
                {
                    OrderId = newOrder.OrderId,
                    RequiredBinWIdth = newOrder.RequiredBinWidth
                });

                // now add the related items for this order
                foreach (var item in newOrder.Items)
                {
                    conn.Execute(@"INSERT INTO [OrderItem] (OrderItemId, OrderId, ProductType, Quantity)
                            VALUES(@OrderItemId, @OrderId, @ProductType, @Quantity)",
                    new
                    {
                        OrderItemId = Guid.NewGuid(),
                        OrderId = newOrder.OrderId,
                        ProductType = item.ProductType,
                        Quantity = item.Quantity
                    });
                }

                trans.Complete();
            }
        }

        /// <summary>
        /// Retrurn the full order details for the order id given
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderDto? GetOrderById(Guid orderId)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                // Fetch the head order details
                var foundOrder = conn.QueryFirstOrDefault<OrderDto>(@"SELECT [OrderId], [Created], [RequiredBinWidth]
                                                                    FROM [Order] WHERE [OrderID] = @OrderId",
                new
                {
                    OrderId = orderId
                });

                if (foundOrder != null)
                {
                    // fetch the associated order items and add to the order header
                    foundOrder.Items = conn.Query<OrderItemDto>(@"SELECT [ProductType], [Quantity]
                                                                    FROM [OrderItem] WHERE [OrderID] = @OrderId",
                    new
                    {
                        OrderId = orderId
                    }).ToList();
                }

                return foundOrder;
            }
        }

    }
}
