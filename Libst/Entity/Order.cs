using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class Order
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetail>();
        }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int TypePayment { get; set; }
        public int StatusPayment { get; set; }
        public int StatusOrder { get; set; }
        public Boolean IsReview { get; set; }   
        public string CreateDate { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
