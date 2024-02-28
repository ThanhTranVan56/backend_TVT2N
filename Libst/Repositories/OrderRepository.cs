using Libs.Data;
using Libs.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Order getOrdercart(Guid id);
        public List<Order> getOrderList();
        public List<Order> getOrderList(string uid);
        public List<Order> getOrderListCXN(string uid);
        public List<Order> getOrderListHT(string uid);
        public List<Order> getCartOrderCGH(string uid);
        public List<Order> getCartOrderTHHT(string uid);
        public List<Order> getCartOrderDH(string uid);
        public List<Order> getCartReviews(string uid);


    }
    internal class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public Order getOrdercart(Guid id)
        {
            return _dbContext.Order.FirstOrDefault(x => x.Id == id);
        }
        public List<Order> getOrderList()
        {
            return _dbContext.Order.ToList()
                    .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                    .ToList();
        }
        public List<Order> getOrderList(string uid)
        {
            return _dbContext.Order.Where(x => x.UserId == uid)
                    .ToList()
                    .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                    .ToList();
        }
        public List<Order> getOrderListCXN(string uid)
        {
            return _dbContext.Order.Where(x => x.UserId == uid && x.StatusOrder == 1)
                    .ToList()
                    .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                    .ToList();
        }
        public List<Order> getCartOrderCGH(string uid)
        {
            return _dbContext.Order.Where(x => x.UserId == uid && x.StatusOrder == 2)
                    .ToList()
                    .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                    .ToList();
        }
        public List<Order> getOrderListHT(string uid)
        {
            return _dbContext.Order.Where(x => x.UserId == uid && x.StatusOrder == 3)
                    .ToList()
                    .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                    .ToList();
        }
        public List<Order> getCartOrderTHHT(string uid)
        {
            return _dbContext.Order.Where(x => x.UserId == uid && x.StatusOrder == 4)
                                .ToList()
                                .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                                .ToList();
        }
        public List<Order> getCartOrderDH(string uid)
        {
            return _dbContext.Order.Where(x => x.UserId == uid && x.StatusOrder == 5)
                                .ToList()
                                .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                                .ToList();
        }
        public List<Order> getCartReviews(string uid)
        {
            return _dbContext.Order.Where(x => x.UserId == uid && x.StatusOrder == 3 && x.IsReview == false)
                                .ToList()
                                .OrderByDescending(x => DateTime.ParseExact(x.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture))
                                .ToList();
        }

    }
}
