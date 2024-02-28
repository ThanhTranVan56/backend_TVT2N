using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class OrderDetailService
    {
        private ApplicationDbContext dbContext;
        private IOrderDetailRepository orderDetailRepository;

        public OrderDetailService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.orderDetailRepository = new OrderDetailRepository(this.dbContext);
        }
        public void Save()
        {
            this.dbContext.SaveChanges();


        }
        public List<OrderDetail> getOrderDetailList(Guid orderId)
        {
            return orderDetailRepository.getOrderDetailList(orderId);
        }
        public List<OrderDetail> getOrderDetailReviews(Guid orderId)
        {
            return orderDetailRepository.getOrderDetailReviews(orderId);
        }
        public void insertOrderDetail(OrderDetail orderDetail)
        {
            dbContext.OrderDetail.Add(orderDetail);
            Save();
        }
        public void updateOrderDetail(OrderDetail orderDetail)
        {
            dbContext.OrderDetail.Update(orderDetail);
            Save();
        }
    }
}
