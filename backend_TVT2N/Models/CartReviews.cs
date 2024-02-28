namespace backend_TVT2N.Models
{
    public class CartReviews
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
