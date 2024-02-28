using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ShoppingCartService shoppingCartService;
        private ProductService productService;
        private OrderService orderService;
        private OrderDetailService orderDetailService;
        public ShoppingCartController(ShoppingCartService shoppingCartService, ProductService productService, OrderService orderService, OrderDetailService orderDetailService)
        {
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
        }
        [HttpGet("get-shoppingcart-list")]
        public IActionResult getShoppingCartList()
        {
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartList();
            return Ok(new { status = true, message = "", data = shoppingCartlist });
        }
        [HttpGet("get-shoppingcart-uid")]
        public IActionResult getShoppingCartUid(string uid)
        {
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartUId(uid);
            foreach(ShoppingCart cart in  shoppingCartlist)
            {
                cart.IsOrder = false;
                shoppingCartService.updateCart(cart);
            }
            List<ShoppingCart> shoppingCartlists = shoppingCartService.getShoppingCartUId(uid);
            return Ok(new { status = true, message = "", data = shoppingCartlists });
        }
        [HttpPost("insert-shoppingcart")]
        public IActionResult insertShoppingCart(InsertShoppingCartModel shoppingCartModel)
        {
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartUId(shoppingCartModel.UserId);
            var flag = false;
            foreach(var cart in shoppingCartlist)
            {
                if(cart.ProductId == shoppingCartModel.ProductId)
                {
                    flag = true;
                    cart.Quantity += shoppingCartModel.Quantity;
                    shoppingCartService.updateCart(cart);
                    int quantitys = shoppingCartService.getQuantity(shoppingCartModel.UserId); 
                    return Ok(new { status = true, message = "success", data = quantitys });
                }
            }
            if(!flag) {
                Product sc = productService.getProuct(shoppingCartModel.ProductId);
                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.Id = Guid.NewGuid();
                shoppingCart.ProductId = shoppingCartModel.ProductId;
                shoppingCart.UserId = shoppingCartModel.UserId;
                shoppingCart.Quantity = shoppingCartModel.Quantity;
                shoppingCart.ProductName = sc.Name;
                shoppingCart.ProductImg = sc.ImageUrl;
                shoppingCart.Price = sc.Price;
                shoppingCart.TotalPrice = sc.Price * shoppingCartModel.Quantity;
                shoppingCart.IsActive = false;
                shoppingCart.IsDelete = false;
                shoppingCart.IsOrder = false;
                shoppingCartService.insertShoppingCart(shoppingCart);
                int quantity = shoppingCartService.getQuantity(shoppingCartModel.UserId);
                return Ok(new { status = true, message = "success", data = quantity });
            }
            return Ok(new { status = false, message = "failure", data = 0 });
        }
        [HttpPost("insert-shoppingcart-buynow")]
        public IActionResult insertShoppingCartBuyNow(InsertShoppingCartModel insertcart)
        {
            Product product = productService.getProuct(insertcart.ProductId);
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.Id = Guid.NewGuid();
            shoppingCart.ProductId = insertcart.ProductId;
            shoppingCart.UserId = insertcart.UserId;
            shoppingCart.Quantity = insertcart.Quantity;
            shoppingCart.ProductName = product.Name;
            shoppingCart.ProductImg = product.ImageUrl;
            shoppingCart.Price = product.Price;
            shoppingCart.TotalPrice = product.Price * insertcart.Quantity;
            shoppingCart.IsActive = true;
            shoppingCart.IsDelete = false;
            shoppingCart.IsOrder = true;
            shoppingCartService.insertShoppingCart(shoppingCart);
            return Ok(new { status = true, message = "success"});
        }
        [HttpPost("insert-shoppingcart-buy-again")]
        public IActionResult insertShoppingCartBuyAgain(Guid orderid, string uid)
        {
            List<OrderDetail> orderDetails = orderDetailService.getOrderDetailList(orderid);
            foreach (OrderDetail orderDetail in orderDetails)
            {
                Product product = productService.getProuct(orderDetail.ProductId);
                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.Id = Guid.NewGuid();
                shoppingCart.ProductId = orderDetail.ProductId;
                shoppingCart.UserId = uid;
                shoppingCart.Quantity = orderDetail.Quantity;
                shoppingCart.ProductName = product.Name;
                shoppingCart.ProductImg = product.ImageUrl;
                shoppingCart.Price = product.Price;
                shoppingCart.TotalPrice = product.Price * orderDetail.Quantity;
                shoppingCart.IsActive = true;
                shoppingCart.IsDelete = false;
                shoppingCart.IsOrder = true;
                shoppingCartService.insertShoppingCart(shoppingCart);
            }
            return Ok(new { status = true, message = "success" });
        }

        [HttpGet("get-shoppingcart-order")]
        public IActionResult getShoppingCartOrder(string uid)
        {
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartIsOrder(uid);
            return Ok(new { status = true, message = "", data = shoppingCartlist });
        }
        [HttpPost("delete-item-order")]
        public IActionResult deleteItemOrder(Guid id)
        {
            ShoppingCart cart = shoppingCartService.getShoppingCart(id);
            cart.IsOrder = false;
            shoppingCartService.updateCart(cart);
            return Ok(new { status = true, message = "success" });
        }
        [HttpPost("delete-list-cart-order")]
        public IActionResult deleteListCartOrder(string uid)
        {
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartIsOrder(uid);
            foreach (ShoppingCart cart in shoppingCartlist)
            {
                cart.IsOrder = true;
                shoppingCartService.updateCart(cart);
            }
            return Ok(new { status = true, message = "success" });
        }
        [HttpPost("update-isorder-cart")]
        public IActionResult updateIsOrder(string uid)
        {
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartUIdOrder(uid);
            foreach(ShoppingCart cart in shoppingCartlist)
            {
                cart.IsOrder = true;
                shoppingCartService.updateCart(cart);
            }
            return Ok(new { status = true, message = "success" });
        }

        [HttpGet("get-shoppingcart-quantity")]
        public IActionResult getShoppingCartQuantity(string uid)
        {
            int quantity = shoppingCartService.getQuantity(uid);
            
            return Ok(new { status = true, message = "success", data = quantity });
        }

        [HttpGet("get-totalprice-cart")]
        public IActionResult getTotalPrice(string uid)
        {
            Decimal totalPrice = 0;
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartUId(uid);
            foreach(var cart in shoppingCartlist)
            {
                if (cart.IsActive)
                {
                    totalPrice += cart.TotalPrice;
                }
            }
            return Ok(new { status = true, message = "success", data = totalPrice });
        }
        [HttpGet("get-totalprice-cart-order")]
        public IActionResult getTotalPriceOrder(string uid)
        {
            Decimal totalPrice = 0;
            List<ShoppingCart> shoppingCartlist = shoppingCartService.getShoppingCartIsOrder(uid);
            foreach (var cart in shoppingCartlist)
            {
                totalPrice += cart.TotalPrice;
            }
            return Ok(new { status = true, message = "success", data = totalPrice });
        }

        [HttpPost("update-isactive-cart")]
        public IActionResult updateIsActive(Guid id)
        {
            ShoppingCart cart = shoppingCartService.getShoppingCart(id);
            cart.IsActive = !cart.IsActive;
            shoppingCartService.updateCart(cart);
            return Ok(new { status = true, message = "success"});
        }
        [HttpPost("update-isdelete-cart")]
        public IActionResult updateIsDelete(Guid id)
        {
            ShoppingCart cart = shoppingCartService.getShoppingCart(id);
            cart.IsDelete = !cart.IsDelete;
            shoppingCartService.updateCart(cart);
            return Ok(new { status = true, message = "success" });
        }
        [HttpPost("update-quantity-cart")]
        public IActionResult updateQuantity(Guid id, int quantity)
        {
            ShoppingCart cart = shoppingCartService.getShoppingCart(id);
            cart.Quantity = quantity;
            cart.TotalPrice = cart.Price * quantity;
            shoppingCartService.updateCart(cart);
            return Ok(new { status = true, message = "success" });
        }
    }
}
