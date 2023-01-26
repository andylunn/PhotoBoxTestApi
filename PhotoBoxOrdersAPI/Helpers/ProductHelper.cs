using PhotoBoxOrdersAPI.Models;

namespace PhotoBoxOrdersAPI.Helpers
{
    public static class ProductHelper
    {
        private static ProductDefinition[] _definitions =
        {
            new ProductDefinition() { ProductType = ProductType.PhotoBook, Width = 19, ItemsPerStack = 1 },
            new ProductDefinition() { ProductType = ProductType.Calendar, Width = 10, ItemsPerStack = 1 },
            new ProductDefinition() { ProductType = ProductType.Canvas, Width = 16, ItemsPerStack = 1 },
            new ProductDefinition() { ProductType = ProductType.Mug, Width = 94, ItemsPerStack = 4 },   // Mugs can be stacked up to 4 in the same width
            new ProductDefinition() { ProductType = ProductType.Card, Width = 4.7M, ItemsPerStack = 1 }
        };

        /// <summary>
        /// Calculate the total width required to fit all the items in an order
        /// taking into account product types that allow for more than one item to be stacked
        /// in the same width
        /// </summary>
        /// <param name="items">Items to calculate total width for</param>
        /// <returns>Total width (mm)</returns>
        public static decimal CalculateMinimumWidth(List<OrderItemDto> items)
        {
            decimal minimumWidth = 0;

            foreach (var i in items)
            {
                var def = _definitions.First(d => d.ProductType == i.ProductType);
                minimumWidth += def.Width * Math.Round(((decimal)i.Quantity / (decimal)def.ItemsPerStack),
                    MidpointRounding.ToPositiveInfinity);
            }

            return minimumWidth;
        }
    }
}
