using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class OrderService
    {
        private ApplicationDbContext dbContext;
        private IOrderRepository orderRepository;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.orderRepository = new OrderRepository(this.dbContext);
        }
        public void Save()
        {
            this.dbContext.SaveChanges();


        }
        public Order getOrdercart(Guid id)
        {
            return orderRepository.getOrdercart(id);
        }
        public List<Order> getOrderList()
        {
            return orderRepository.getOrderList();
        }
        public List<Order> getOrderList(string uid)
        {
            return orderRepository.getOrderList(uid);
        }
        public List<Order> getCartOrderCXN(string uid)
        {
            return orderRepository.getOrderListCXN(uid);
        }
        public List<Order> getCartOrderCGH(string uid)
        {
            return orderRepository.getCartOrderCGH(uid);
        }
        public List<Order> getCartOrderHT(string uid)
        {
            return orderRepository.getOrderListHT(uid);
        }
        public List<Order> getCartOrderHTTH(string uid)
        {
            return orderRepository.getCartOrderTHHT(uid);
        }
        public List<Order> getCartOrderDH(string uid)
        {
            return orderRepository.getCartOrderDH(uid);
        }
        public List<Order> getCartReviews(string uid)
        {
            return orderRepository.getCartReviews(uid);
        }
        public void insertOrder(Order order)
        {
            dbContext.Order.Add(order);
            Save();
        }
        public void updateOrder(Order order) { 
            dbContext.Order.Update(order); 
            Save();
        }
    }
}
