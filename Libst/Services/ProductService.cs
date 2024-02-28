using Libs.Entity;
using Libs.Repositories;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class ProductService
    {
        private ApplicationDbContext dbContext;
        private IProductRepository productRepository;

        public ProductService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
            this.productRepository = new ProductRepository(this.dbContext);
        }
        public void Save() { 
            this.dbContext.SaveChanges();

        }
        public Product getProuct(Guid id)
        {
            return productRepository.getProduct(id);
        }
        public List<Product> getProductList() { 
            return productRepository.getProductList();
        }
        public List<Product> getProductNew()
        {
            return productRepository.getProductNew();
        }
        public List<Product> getProductFavourite()
        {
            return productRepository.getProductFavourite();
        }
        public List<Product> getProductCategory(Guid idcate)
        {
            return productRepository.getProductCategory(idcate);
        }
        public List<Product> getProductSimilar(Guid idcate, Guid idpro)
        {
            return productRepository.getProductSimilar(idcate, idpro);
        }
        public List<Product> searchProduct(string txtsearch)
        {
            return productRepository.searchProduct(txtsearch);
        }
        public void insertProduct(Product product) {
            dbContext.Product.Add(product);
            Save();
        }
        public void updateProduct(Product product)
        {
            dbContext.Product.Update(product);
            Save();
        }
    }
}
