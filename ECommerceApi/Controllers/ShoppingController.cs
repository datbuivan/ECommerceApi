using ECommerceApi.DataAccess;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ECommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IDataAccess dataAccess;
        private readonly string DateFormat;
        public ShoppingController(IDataAccess _dataAccess, IConfiguration configuration)
        {
            this.dataAccess = _dataAccess;
            DateFormat = configuration["Constants:DateFormat"];
        }
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await dataAccess.GetProductCategories();
            if(categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        [HttpGet]
        [Route("GetProductCategory/{Id}")]
        public async Task<IActionResult> GetProductCategory(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productCategory = await dataAccess.GetProductCategory(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return Ok(productCategory);

        }
        [HttpGet]
        [Route("GetOffer/{id}")]
        public async Task<IActionResult> GetOffer(int? id)
        {
            if (id == null) { 
                return NotFound();
            }
            var offer = await dataAccess.GetOffer(id);
            if (offer == null)
            {
                return NotFound();
            }
            return Ok(offer);   
            
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts(string category, string subcategory, int count)
        {
            var products = await dataAccess.GetProducts(category,subcategory,count);
            if(products == null)
            {
                return NotFound();
            }
            return Ok(products);
            
        }

        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await dataAccess.GetProductById(id);  
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductReviews/{productid}")]
        public async Task<IActionResult> GetProductReviews(int? productid)
        {
            if(productid == null)
            {
                return NotFound();
            }
            var productreviews = await dataAccess.GetproductReview(productid);
            if(productreviews == null) return NotFound();
            return Ok(productreviews);

        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int? id)
        {
            if(id== null)
            {
                return NotFound(nameof(id));
            }
            var user = await dataAccess.GetUser(id);    
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet]
        [Route("GetAllPreviousCartOfUser/{userid}")]
        public async Task<IActionResult> GetAllPreviousCartOfUser(int? userid)
        {
            if (userid == null)
            {
                return NotFound();
            }
            var listcart = await dataAccess.GetAllPreviousCartOfUser(userid);
            if(listcart == null)
            {
                return NotFound();
            }
            return Ok(listcart);
        }
        [HttpGet]
        [Route("GetActiveCartOfUser/{userid}")]
        public async Task<IActionResult> GetActiveCartOfUser(int? userid)
        {
            if (userid== null)
            {
                return NotFound();
            }
            var result = await dataAccess.GetActiveCartOfUser(userid);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetPaymentMethods")]
        public async Task<IActionResult> GetPaymentMethods()
        {
            try
            {
                var results = await dataAccess.GetPaymentMethods(); 
                return Ok(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);  
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            string message= "";
            user.CreateAt = DateTime.Now.ToString(DateFormat);
            user.ModifiedAt = DateTime.Now.ToString(DateFormat);
            var result = await dataAccess.AddUser(user);
            if (result==true) message = "Added Successfully";
            else message = "email not available";
            return Ok(message);

        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] User user)
        {
            var token = dataAccess.IsUserPresent(user.Email, user.Password);
            if (token == "") token = "invalid";
            return Ok(token);
        }
        [HttpPost]
        [Route("InsertReview")]
        public async Task<IActionResult> InsertReview([FromBody] Review review)
        {
            review.CreateAt = DateTime.Now.ToString(DateFormat);
            var result = await dataAccess.InsertReview(review);
            if (result)
            {
                return Ok("False");
            }
            return Ok("Inserted");
        }

        [HttpPost]
        [Route("InsertCartItem/{userid}/{productid}")]
        public async Task<IActionResult> InsertCartItem(int userid, int productid)
        {
            var result = await dataAccess.InserCartItem(userid, productid);
            return Ok(result? "Inserted" : "Not Inserted");
        }

        [HttpPost]
        [Route("InsertPayment")]
        public async Task<IActionResult> InsertPayment(Payment payment)
        {
            payment.CreateAt = DateTime.Now.ToString(DateFormat);
            var result = await dataAccess.InsertPayment(payment);
            if(result > 0)
            {
                return Ok(payment.Id);
            }
            return Ok("Faild");
        }

        [HttpPost]
        [Route("InsertOrder")]
        public async Task<IActionResult> InsertOrder(Order order)
        {
            order.CreateAt = DateTime.Now.ToString(DateFormat);
            var result = await dataAccess.InsertOrder(order);
            return Ok(result.ToString());
        }


    }
}
