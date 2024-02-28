namespace backend_TVT2N.Models
{
    public class InsertShoppingCartModel
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
