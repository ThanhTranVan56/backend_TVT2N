using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace backend_TVT2N.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private OrderService orderService;
        private OrderDetailService orderDetailService;
        private AddressService addressService;
        private ProductService productService;
        private ShoppingCartService shoppingCartService;
        private const int pageSize = 10;
        public HomeController(ILogger<HomeController> logger, ShoppingCartService shoppingCartService, ProductService productService, OrderService orderService, OrderDetailService orderDetailService, AddressService addressService)
        {
            _logger = logger;
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
            this.addressService = addressService;
        }

        public IActionResult Index(int? page)
        {

            IPagedList<Order> orders = GetPagedNames(page);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;
            return View(orders);
        }
        public IActionResult Details(Guid id)
        {
            Order order = orderService.getOrdercart(id);
            order.OrderDetails = orderDetailService.getOrderDetailList(order.Id);
            return View(order);
        }
        public IActionResult ListDetail(Guid id)
        {
            IEnumerable<OrderDetail> orderDetails = orderDetailService.getOrderDetailList(id);

            return View(orderDetails);
        }
        [HttpPost]
        public IActionResult UpdateTT(Guid id, int statuspayment, int statusorder)
        {
            Order item = orderService.getOrdercart(id);
            item.StatusPayment = statuspayment;
            item.StatusOrder = statusorder;
            orderService.updateOrder(item);
            return Json(new { message = "Success", Success = true });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private IPagedList<Order> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
            {
                return null;
            }

            // retrieve list from database/whereverand
            var listUnPaged = GetStuffFromFile();

            // page the list

            var listPaged = listUnPaged.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
            {
                return null;
            }

            return listPaged;
        }

        /// <summary>
        /// In this case we return array of string, but in most DB situations you'll want to return IQueryable
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Order> GetStuffFromFile()
        {
            IEnumerable<Order> orders = orderService.getOrderList();
            return orders;
        }
    }
}