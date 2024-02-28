﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Boolean IsReviewed {  get; set; }
    }
}
