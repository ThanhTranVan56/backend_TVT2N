using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsOrder { get; set; }
        
        //public string Code { get; set; }
    }
}
