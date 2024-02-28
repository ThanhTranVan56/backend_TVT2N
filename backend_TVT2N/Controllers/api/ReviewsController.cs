using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private ReviewsService reviewService;
        private ProfileService profileService;
        private OrderService orderService;
        private OrderDetailService orderDetailService;
        private ProductService productService;
        public ReviewsController(ReviewsService reviewService, ProfileService profileService, OrderService orderService, OrderDetailService orderDetailService, ProductService productService)
        {
            this.reviewService = reviewService;
            this.profileService = profileService;
            this.orderService = orderService;
            this.orderDetailService = orderDetailService;
            this.productService = productService;
        }
        [HttpPost("insert-review")]
        public IActionResult insertReview(InsertReview insertReview)
        {
            Reviews reviews = new Reviews();
            reviews.Id = Guid.NewGuid();
            reviews.UserId = insertReview.UserId;
            reviews.ProductId = insertReview.ProductId;
            reviews.Rating = insertReview.Rating;
            reviews.Outstanding = insertReview.Outstanding;
            reviews.Quality = insertReview.Quality;
            reviews.Comment = insertReview.Comment;
            reviews.IsName = insertReview.IsName;
            Profile profile = profileService.getProfile(insertReview.UserId);
            reviews.UserName = profile.Name;
            reviews.UserImg = profile.Image;
            DateTime dateTime = DateTime.Now;
            reviews.CreateDate = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            reviewService.insertReviews(reviews);

            int flag = 0;
            List<OrderDetail> orderDetails = orderDetailService.getOrderDetailList(insertReview.OrderId);
            foreach (OrderDetail orderDetail in orderDetails)
            {
                if (orderDetail.IsReviewed == false)
                {
                    flag++;
                }
                if(orderDetail.ProductId == insertReview.ProductId)
                {
                    orderDetail.IsReviewed = true;
                    orderDetailService.updateOrderDetail(orderDetail);
                }      
            }
            if(flag == 1) {
                Order order = orderService.getOrdercart(insertReview.OrderId);
                order.IsReview = true;
                orderService.updateOrder(order);
            }

            List<Reviews> reviewlist = reviewService.getReviewsPro(insertReview.ProductId);
            int totalRating = 0;
            int count = 0;
            foreach (Reviews rv in reviewlist)
            {
                totalRating += rv.Rating;
                count++;
            }
            double averageRating = (double)totalRating / count;
            int roundedRating = (int)Math.Round(averageRating);
            Product pro = productService.getProuct(insertReview.ProductId);
            pro.Rating = roundedRating;
            productService.updateProduct(pro);
            return Ok(new { status = true, message = "success" });
        }
        [HttpGet("get-reviews")]
        public IActionResult getReviews(Guid idpro)
        {
            List<Reviews> reviews = reviewService.getReviewsPro(idpro);
            return Ok(new { status = true, message = "success", data = reviews });
        }
        [HttpGet("get-sl-reviews")]
        public IActionResult getSLReviews(Guid idpro)
        {
            List<Reviews> reviews = reviewService.getReviewsPro(idpro);
            return Ok(new { status = true, message = "success", data = reviews.Count() });
        }
    }
}

        