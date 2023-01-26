using PhotoBoxOrdersAPI.Helpers;
using PhotoBoxOrdersAPI.Models;

namespace PhotoBoxOrdersAPITest
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        [DataRow(1, 2, 0, 0, 1, 133.0)]
        [DataRow(1, 2, 0, 0, 2, 133.0)]     // Testing 2 mugs takes up the same space as 1 due to stacking
        [DataRow(0, 0, 8, 1, 1, 226.7)]     // Decimal check
        [DataRow(1, 2, 8, 1, 2, 265.7)]     // ^^
        [DataRow(1, 2, 8, 0, 3, 261.0)]     // Increasing mug count by one keeps width the same are prior
        [DataRow(1, 2, 8, 0, 4, 261.0)]     // Increasing again stays the same (but at maximum stacking)
        [DataRow(1, 2, 8, 0, 5, 355.0)]     // Tiping over the edge of maximum stacking increases width by 1 mug more
        [DataRow(1, 2, 8, 0, 8, 355.0)]     // Same again but at capacity of 2 stacks worth of mugs
        [DataRow(2, 2, 8, 1, 21, 754.7)]    // And finally off into large volume
        public void CorrectMinWidthForOrderItems(int photoBookCount, int calendarCount, int canvasCount, int cardCount, int mugCount, double expectedWidth)
        {
            List<OrderItemDto> items = new List<OrderItemDto>();

            if (photoBookCount > 0) items.Add(new OrderItemDto() { ProductType = ProductType.PhotoBook, Quantity = photoBookCount });
            if (calendarCount > 0) items.Add(new OrderItemDto() { ProductType = ProductType.Calendar, Quantity = calendarCount });
            if (canvasCount > 0) items.Add(new OrderItemDto() { ProductType = ProductType.Canvas, Quantity = canvasCount });
            if (cardCount > 0) items.Add(new OrderItemDto() { ProductType = ProductType.Card, Quantity = cardCount });
            if (mugCount > 0) items.Add(new OrderItemDto() { ProductType = ProductType.Mug, Quantity = mugCount });

            var testWidth = ProductHelper.CalculateMinimumWidth(items);

            Assert.AreEqual((decimal)expectedWidth, testWidth);
        }
    }
}