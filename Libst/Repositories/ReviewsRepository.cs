using Libs.Data;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IReviewsRepository : IRepository<Reviews>
    {
        public List<Reviews> getReviewsPro(Guid idpro);
    }
    public class ReviewsRepository : RepositoryBase<Reviews>, IReviewsRepository
    {
        public ReviewsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Reviews> getReviewsPro(Guid idpro)
        {
            return _dbContext.Reviews.Where(x=>x.ProductId == idpro).ToList();
        }
    }
}
