using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService categoryService;
        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet("get-category-list")]
        public IActionResult getProductList()
        {
            List<Category> categoryList = categoryService.getCategoryList();
            return Ok(new { status = true, message = "success", data = categoryList });
        }
    }
}
