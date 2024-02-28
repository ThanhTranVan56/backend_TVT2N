using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string UserId {  get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Phone {  get; set; }
        public string Email {  get; set; }
        public int TypePayment {  get; set; }
    }
}
