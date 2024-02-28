using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class Reviews
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public string UserName { get; set; }
        public string UserImg { get; set; }
        public int Rating { get; set; }
        public string Outstanding {  get; set; }
        public string Quality {  get; set; }
        public string Comment {  get; set; }
        public Boolean IsName {  get; set; }
        public string CreateDate { get; set; }
    }
}
