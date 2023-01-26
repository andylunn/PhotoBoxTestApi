namespace PhotoBoxOrdersAPI.Models
{
    public enum ProductType
    {
        PhotoBook = 1,
        Calendar = 2,
        Canvas = 3,
        Card = 4,
        Mug = 5
    }
    public class ProductDefinition
    {
        public ProductType ProductType { get; set; }
        public decimal Width { get; set; }
        public int ItemsPerStack { get; set; }
    }
}
