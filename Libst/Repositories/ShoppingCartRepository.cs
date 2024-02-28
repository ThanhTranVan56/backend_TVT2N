using Libs.Data;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        public List<ShoppingCart> getShoppingCartList();
        public List<ShoppingCart> getShoppingCartUId(string uid);
        public List<ShoppingCart> getShoppingCartUIdOrder(string uid);
        public List<ShoppingCart> getShoppingCartIsOrder(string uid);
        public ShoppingCart getShoppingCart(Guid id);
    }
    public class ShoppingCartRepository : RepositoryBase<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<ShoppingCart> getShoppingCartList()
        {
            return _dbContext.ShoppingCart.Where(x=>x.IsDelete == false).ToList();
        }

        public List<ShoppingCart> getShoppingCartUId(string uid)
        {
            return _dbContext.ShoppingCart.Where(x => x.UserId == uid && x.IsDelete == false).ToList();
        }
        public List<ShoppingCart> getShoppingCartUIdOrder(string uid)
        {
            return _dbContext.ShoppingCart.Where(x => x.UserId == uid && x.IsDelete == false && x.IsActive == true).ToList();
        }
        public List<ShoppingCart> getShoppingCartIsOrder(string uid)
        {
            return _dbContext.ShoppingCart.Where(x => x.UserId == uid && x.IsDelete == false && x.IsOrder == true).ToList();
        }
        public ShoppingCart getShoppingCart(Guid id)
        {
            return _dbContext.ShoppingCart.FirstOrDefault(x => x.Id == id);
        }
    }
}
