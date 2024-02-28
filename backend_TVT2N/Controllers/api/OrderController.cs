using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ShoppingCartService shoppingCartService;
        private ProductService productService;
        private OrderService orderService;
        private OrderDetailService orderDetailService;
        private AddressService addressService;
        private ProfileService profileService;
        public OrderController(ShoppingCartService shoppingCartService, ProductService productService, OrderService orderService, OrderDetailService orderDetailService, AddressService addressService, ProfileService profileService)
        {
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
            this.addressService = addressService;
            this.profileService = profileService;
        }
        [HttpPost("check-out-cart-order")]
        public IActionResult checkoutCartOrder(string uid, string code)
        {
            List<ShoppingCart> shoppingCartlistorder = shoppingCartService.getShoppingCartIsOrder(uid);
            Address address = addressService.getAddressIsDefault(uid);
            Profile profile = profileService.getProfile(uid);
            if (shoppingCartlistorder.Any() && shoppingCartlistorder != null)
            {
                Order order = new Order();
                order.Id = Guid.NewGuid();
                order.UserId = uid;
                order.Name = address.Name;
                order.Phone = address.Phone;
                order.Email = address.Email;
                order.Address = address.Addres;
                order.TypePayment = profile.TypePayment; // 1 Code , 2 Online
                order.StatusOrder = 1; //1,/ Chờ xác nhận,2 / chờ giao hàng, 3 / Hoàn thành, 4 / trả hàng/hoàn tiền ,5 / hủy
                if(profile.TypePayment == 1)
                {
                    order.StatusPayment = 1;//chưa thanh toán, 2/đã thanh toán ,3/ hủy
                } else
                {
                    order.StatusPayment = 2;
                }
                DateTime dateTime = DateTime.Now;
                order.CreateDate = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
                decimal ttprice = 0;
                int sl = 0;
                foreach (ShoppingCart cart in shoppingCartlistorder)
                {
                    sl++;
                    ttprice += cart.TotalPrice;
                    cart.IsDelete = true;
                    cart.IsOrder = false;
                    shoppingCartService.updateCart(cart);
                }
                order.TotalPrice = ttprice;
                order.Quantity = sl;
                order.Code = code;
                order.IsReview = false;
                shoppingCartlistorder.ForEach(x => order.OrderDetails.Add(new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductImg =x.ProductImg,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    IsReviewed = false

                }));
                orderService.insertOrder(order);

                return Ok(new { status = true, message = "success"});
            }
            return Ok(new { status = false, message = "failed"});
        }
        [HttpGet("get-cart-order-list")]
        public IActionResult getCartOrderList(string uid)
        {
            List<Order> orders = orderService.getOrderList(uid);
            if(orders != null && orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailReviews(order.Id);
                }
                return Ok(new { status = true, message = "success", data = orders });
            }
            return Ok(new { status = false, message = "failed" , data = orders });
        }
        [HttpGet("get-cart-order-wait-confirm")]
        public IActionResult getCartOrderCXN(string uid)
        {
            List<Order> orders = orderService.getCartOrderCXN(uid);
            if (orders != null && orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailReviews(order.Id);
                }
                return Ok(new { status = true, message = "success", data = orders });
            }
            return Ok(new { status = false, message = "failed", data = orders });
        }
        [HttpGet("get-sl-wait-confirm")]
        public IActionResult getSLCartOrderCXN(string uid)
        {
            List<Order> orders = orderService.getCartOrderCXN(uid);
            
            return Ok(new { status = true, message = "success", data = orders.Count() });
        }
        [HttpGet("get-cart-order-wait-ship")]
        public IActionResult getCartOrderCGH(string uid)
        {
            List<Order> orders = orderService.getCartOrderCGH(uid);
            if (orders != null && orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailList(order.Id);
                }
                return Ok(new { status = true, message = "success", data = orders });
            }
            return Ok(new { status = false, message = "failed", data = orders });
        }
        [HttpGet("get-sl-wait-ship")]
        public IActionResult getSLCartOrderCGH(string uid)
        {
            List<Order> orders = orderService.getCartOrderCGH(uid);
            return Ok(new { status = true, message = "success", data = orders.Count() });
        }
        [HttpGet("get-cart-order-complete")]
        public IActionResult getCartOrderHT(string uid)
        {
            List<Order> orders = orderService.getCartOrderHT(uid);
            if (orders != null && orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailList(order.Id);
                }
                return Ok(new { status = true, message = "success", data = orders });
            }
            return Ok(new { status = false, message = "failed", data = orders });
        }
        [HttpGet("get-cart-order-Refund")]
        public IActionResult getCartOrderHTTH(string uid)
        {
            List<Order> orders = orderService.getCartOrderHTTH(uid);
            if (orders != null && orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailList(order.Id);
                }
                return Ok(new { status = true, message = "success", data = orders });
            }
            return Ok(new { status = false, message = "failed", data = orders });
        }
        [HttpGet("get-cart-order-cancel")]
        public IActionResult getCartOrderDH(string uid)
        {
            List<Order> orders = orderService.getCartOrderDH(uid);
            if (orders != null && orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailList(order.Id);
                }
                return Ok(new { status = true, message = "success", data = orders });
            }
            return Ok(new { status = false, message = "failed", data = orders });
        }
        [HttpGet("get-cart-reviews")]
        public IActionResult getCartReviews(string uid)
        {
            List<Order> orders = orderService.getCartReviews(uid);
            if (orders != null && orders.Count > 0)
            {
                List<CartReviews> cartReviews = new List<CartReviews>();
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailList(order.Id);
                    foreach (OrderDetail orderDetail in order.OrderDetails)
                    {
                        if(orderDetail.IsReviewed == false) {
                            Product product = productService.getProuct(orderDetail.ProductId);
                            cartReviews.Add(new CartReviews
                            {
                                ProductId = product.Id,
                                OrderId = order.Id,
                                Name = product.Name,
                                Price = product.Price,
                                ImageUrl = product.ImageUrl
                            });
                        }
                    }
                }
                return Ok(new { status = true, message = "success", data = cartReviews });
            }
            return Ok(new { status = false, message = "failed", data = orders });
        }
        [HttpGet("get-sl-reviews")]
        public IActionResult getSLCartReviews(string uid)
        {
            List<Order> orders = orderService.getCartReviews(uid);
            if (orders != null && orders.Count > 0)
            {
                List<Product> products = new List<Product>();
                foreach (Order order in orders)
                {
                    order.OrderDetails = orderDetailService.getOrderDetailList(order.Id);
                    foreach (OrderDetail orderDetail in order.OrderDetails)
                    {
                        if (orderDetail.IsReviewed == false)
                        {
                            products.Add(productService.getProuct(orderDetail.ProductId));
                        }
                    }
                }
                return Ok(new { status = true, message = "success", data = products.Count() });
            }
            return Ok(new { status = false, message = "failed", data = 0 });
        }
            //private string randomCode()
            //{
            //    Random rd = new Random();
            //    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            //    string code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + chars[rd.Next(0, chars.Length)] + chars[rd.Next(0, chars.Length)];
            //    return code;
            //}
    }
}
