using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class CategoryService
    {
        private ApplicationDbContext dbContext;
        private ICategoryRepository categoryRepository;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.categoryRepository = new CategoryRepository(this.dbContext);
        }
        public void Save()
        {
            this.dbContext.SaveChanges();


        }
        public List<Category> getCategoryList()
        {
            return categoryRepository.getCategoryList();
        }
        public void insertCategory(Category category)
        {
            dbContext.Category.Add(category);
            Save();
        }
    }
}
