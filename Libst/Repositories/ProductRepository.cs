using Libs.Data;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IProductRepository : IRepository<Product> {
        public Product getProduct(Guid id);
        public List<Product> getProductList();
        public List<Product> getProductNew();
        public List<Product> getProductFavourite();
        public List<Product> getProductCategory(Guid idcate);
        public List<Product> getProductSimilar(Guid idcate, Guid idpro);
        public List<Product> searchProduct(string txtsearch);
        
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public Product getProduct(Guid id)
        {
            return _dbContext.Product.FirstOrDefault(x => x.Id == id);
        }
        public List<Product> getProductList() {
            return _dbContext.Product.ToList();
        }
        public List<Product> getProductNew() {
            return _dbContext.Product.Where(x=>x.IsNew == true).ToList();
        }
        public List<Product> getProductFavourite()
        {
            return _dbContext.Product.Where(x => x.IsFavourite == true).ToList();
        }
        public List<Product> getProductCategory(Guid idcate)
        {
            return _dbContext.Product.Where(x => x.CategoryId == idcate).ToList();
        }
        public List<Product> getProductSimilar(Guid idcate, Guid idpro)
        {
            return _dbContext.Product.Where(x => x.CategoryId == idcate && x.Id != idpro).ToList();
        }
        public List<Product> searchProduct(string txtsearch)
        {
            return _dbContext.Product
                .Where(x => x.Name.ToLower().Contains(txtsearch.ToLower()))
                .ToList();
        }

    }
}
