using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class ShoppingCartService
    {
        private ApplicationDbContext dbContext;
        private IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.shoppingCartRepository = new ShoppingCartRepository(this.dbContext);
        }
        public List<ShoppingCart> getShoppingCartList()
        {
            return shoppingCartRepository.getShoppingCartList();
        }
        public int getQuantity(string uid)
        {
            return shoppingCartRepository.getShoppingCartUId(uid).Count;
        }
        public List<ShoppingCart> getShoppingCartUId(string uid)
        {
            return shoppingCartRepository.getShoppingCartUId(uid);
        }
        public List<ShoppingCart> getShoppingCartUIdOrder(string uid)
        {
            return shoppingCartRepository.getShoppingCartUIdOrder(uid);
        }
        public List<ShoppingCart> getShoppingCartIsOrder(string uid)
        {
            return shoppingCartRepository.getShoppingCartIsOrder(uid);
        }
        public void insertShoppingCart(ShoppingCart shoppingCart)
        {
            dbContext.ShoppingCart.Add(shoppingCart);
            Save();
        }
        public ShoppingCart getShoppingCart(Guid id)
        {
            return shoppingCartRepository.getShoppingCart(id);
        }
        public void updateCart(ShoppingCart shoppingCart)
        {
            dbContext.ShoppingCart.Update(shoppingCart);
            Save();
        }
        public void Save()
        {
            this.dbContext.SaveChanges();
        }

    }
}
