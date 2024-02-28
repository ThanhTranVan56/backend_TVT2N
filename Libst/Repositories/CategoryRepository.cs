using Libs.Data;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public List<Category> getCategoryList();
    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Category> getCategoryList()
        {
            return _dbContext.Category.ToList();
        }
    }
}
