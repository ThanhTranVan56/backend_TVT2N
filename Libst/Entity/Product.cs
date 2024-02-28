using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId {  get; set; }
        public Boolean IsNew { get; set; }
        public Boolean IsHot { get; set; }
        public Boolean IsFavourite { get; set; }
        public int Rating {  get; set; }
    }
}
