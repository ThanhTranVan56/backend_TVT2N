namespace backend_TVT2N.Models
{
    public class InsertReview
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }   
        public int Rating { get; set; }
        public string Outstanding { get; set; }
        public string Quality { get; set; }
        public string Comment { get; set; }
        public Boolean IsName { get; set; }
    }
}
