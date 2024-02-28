using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class ReviewsService
    {
        private ApplicationDbContext dbContext;
        private IReviewsRepository reviewsRepository;

        public ReviewsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.reviewsRepository = new ReviewsRepository(this.dbContext);
        }
        public List<Reviews> getReviewsPro(Guid idpro)
        {
            return reviewsRepository.getReviewsPro(idpro);
        }
        public void insertReviews(Reviews reviews)
        {
            dbContext.Reviews.Add(reviews);
            Save();
        }
        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
