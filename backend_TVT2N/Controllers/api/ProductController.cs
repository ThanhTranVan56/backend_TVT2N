using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductService productService;

        public ProductController(ProductService productService) {
            this.productService = productService;
        }
        [HttpGet("get-product-list")]
        //[Authorize(Policy = "Product.View")]
        //[Authorize(Roles ="admin")]
        public IActionResult getProductList() {
            List<Product> productList = productService.getProductList();
            return Ok(new { status = true, message = "", data = productList});
        }
        [HttpGet("get-product-new")]
        public IActionResult getProductNew()
        {
            List<Product> productList = productService.getProductNew();
            return Ok(new { status = true, message = "", data = productList });
        }
        [HttpGet("get-product-favourite")]
        public IActionResult getProductFavourite()
        {
            List<Product> productList = productService.getProductFavourite();
            return Ok(new { status = true, message = "", data = productList });
        }
        [HttpGet("get-product-category")]
        public IActionResult getProductCategory(Guid idcate)
        {
            List<Product> productList = productService.getProductCategory(idcate);
            return Ok(new { status = true, message = "", data = productList });
        }
        [HttpGet("get-product-similar")]
        public IActionResult getProductSimilar(Guid idcate,Guid idpro)
        {
            List<Product> productList = productService.getProductSimilar(idcate,idpro);
            return Ok(new { status = true, message = "", data = productList });
        }

        [HttpPost("insert-product")]
        public IActionResult insertProduct(InsertProductModel productModel)
        {
            Product product = new Product();
            product.Id = Guid.NewGuid();
            product.Name = productModel.Name;
            product.Price = productModel.Price;


            productService.insertProduct(product);
            return Ok(new { status = true, message = "" });
        }

        [HttpGet("search-product")]
        public IActionResult searchProduct(string txtsearch)
        {
            List<Product> productList = productService.searchProduct(txtsearch);
            return Ok(new { status = true, message = "", data = productList });
        }
        [HttpGet("get-product")]
        //[Authorize(Policy = "Product.View")]
        //[Authorize(Roles ="admin")]
        public IActionResult getProduct(Guid id)
        {
            Product product = new Product();
            return Ok(new { status = true, message = "", data = product });
        }
    }
}
