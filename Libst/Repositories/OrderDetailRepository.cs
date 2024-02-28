using Libs.Data;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        public List<OrderDetail> getOrderDetailList(Guid orderId);
        public List<OrderDetail> getOrderDetailReviews(Guid orderId);
    }
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<OrderDetail> getOrderDetailList(Guid orderId)
        {
            return _dbContext.OrderDetail.Where(x=>x.OrderId == orderId).ToList();
        }
        public List<OrderDetail> getOrderDetailReviews(Guid orderId)
        {
            return _dbContext.OrderDetail.Where(x => x.OrderId == orderId && x.IsReviewed == false).ToList();
        }
    }
}
