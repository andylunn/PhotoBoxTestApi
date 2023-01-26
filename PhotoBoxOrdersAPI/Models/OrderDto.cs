namespace PhotoBoxOrdersAPI.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public decimal RequiredBinWidth { get; set; }
        public List<OrderItemDto> Items { get; set; }

        public OrderDto()
        {
            Items = new List<OrderItemDto>();
        }
    }
}
