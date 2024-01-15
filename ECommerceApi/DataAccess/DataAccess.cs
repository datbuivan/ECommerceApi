using ECommerceApi.Models;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceApi.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly AppDBContext context;
        public DataAccess(AppDBContext _context)
        {
            context = _context;
        }
        public async Task<bool> AddUser(User user)
        {
            var result = context.Users.Where(u => u.Email == user.Email);
            if(result.Count() >0 )
            {
                return false;
            }
            context.Users.Add(user); 
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<Offer> GetOffer(int? id)
        {
           var offer = await context.Offers.FindAsync(id);
            return offer;
        }
        public async Task<Product> GetProductById(int? id)
        {
            var product = await context.Products.Include(o=>o.Offer).Include(c=>c.ProductCategory).Where(x=>x.Id == id).FirstOrDefaultAsync();
            return product;
        }
        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            var categories = await context.ProductCategories.ToListAsync();
            return categories;
        }
        public async Task<ProductCategory> GetProductCategory(int? id)
        {
            var productCategory = await context.ProductCategories.FindAsync(id);
            return productCategory;
        }
        public async Task<IEnumerable<Product>> GetProducts(string category, string subcategory, int count)
        {
            var categoryId = await context.ProductCategories
                    .Where(p => p.Category == category && p.Subcategory == subcategory)
                    .Select(p=>p.Id)
                    .FirstOrDefaultAsync();
            var products = await context.Products
                    .Where(p => p.CategoryId == categoryId)
                    .Take(count)
                    .ToListAsync();
            foreach(var product in products) {
                var catId = product.CategoryId;
                product.ProductCategory = await GetProductCategory(product.CategoryId);
                product.Offer = await GetOffer(product.OfferId);
            }
            return products;
        }
        public string IsUserPresent(string email, string password)
        {
            var user = (from u in context.Users
                        where u.Email == email && u.Password == password
                        select new User
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            Address = u.Address,
                            Mobile = u.Mobile,
                            CreateAt = u.CreateAt,
                            ModifiedAt = u.ModifiedAt
                        }).FirstOrDefault();
            if(user == null)
            {
                return "";
            }
            string key = "MNU66iBl3T5rh6H52i69";
            string duration = "60";
            var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim("id", user.Id.ToString()),
                    new Claim("firstName", user.FirstName),
                    new Claim("lastName", user.LastName),
                    new Claim("address", user.Address),
                    new Claim("mobile", user.Mobile),
                    new Claim("email", user.Email),
                    new Claim("createAt", user.CreateAt),
                    new Claim("modifiedAt", user.ModifiedAt)
                };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(Int32.Parse(duration)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

        }
        public async Task<bool> InsertReview(Review review)
        {
            context.Reviews.Add(review);
            var result = await context.SaveChangesAsync();
            if(result == 0) { return false; }
            return false;
        }
        public async Task<IEnumerable<Review>> GetproductReview(int? productId)
        {
            var productreviews = await context.Reviews
                             .Include(u=>u.User)
                             .Include(p=>p.Product)
                             .Where(r => r.ProductId == productId)
                             .ToListAsync();
            return productreviews;
        }
        public async Task<User> GetUser(int? id)
        {
            var user = await context.Users.Where(u=> u.Id == id).FirstOrDefaultAsync();
            return user;
        }
        public async Task<bool> InserCartItem(int userid, int productid)
        {
            var cart = (from crt in context.Carts
                       where crt.UserId == userid && crt.Ordered == false
                       select crt).FirstOrDefault();
            if(cart == null )
            {
                Cart newCart = new Cart
                {
                    UserId = userid,
                    Ordered = false,
                    OrdereOn = "",
                };
                context.Carts.Add(newCart);
                await context.SaveChangesAsync();  
            }
            CartItems cartItems = new CartItems
            {
                CartId = cart.Id,
                ProductId = productid,
            };
            context.CartItems.Add(cartItems);
            var result = await context.SaveChangesAsync();
            if(result > 0)
            {
                return true;
            }
            return false;   
            
        }
        public async Task<Cart> GetActiveCartOfUser(int? userid)
        {  
            //var cart = (from crt in context.Carts
            //            where crt.UserId == userid && crt.Ordered == false
            //            select crt).FirstOrDefault();
            var cart = await context.Carts
                    .Include(c => c.CartItems).ThenInclude(p => p.Product).ThenInclude(c=>c.ProductCategory)
                    .Include(c => c.CartItems).ThenInclude(p => p.Product).ThenInclude(c => c.Offer)
                    .Include(u=>u.User)
                    .Where(c=> c.UserId == userid)
                    .Where(c=>c.Ordered == false)
                    .FirstOrDefaultAsync();
            if (cart == null)
            {
                Cart newcart = new Cart();
                return newcart;
            }
            return cart;
           
            
        }
        public async Task<Cart> GetCart(int? cartid)
        {
            var cart = await context.Carts.Include(c=>c.CartItems).Where(c=>c.Id == cartid).FirstOrDefaultAsync();
            return cart;
        }
        public async Task<IEnumerable<Cart>> GetAllPreviousCartOfUser(int? userid)
        {
            var carts = await context.Carts
                    .Include(c => c.CartItems).ThenInclude(u=>u.Product).ThenInclude(c=>c.ProductCategory)
                    .Include(c => c.CartItems).ThenInclude(u => u.Product).ThenInclude(c => c.Offer)
                    .Include(u => u.User)
                    .Where(c => c.UserId == userid)
                    .Where(c => c.Ordered == true)
                    .ToListAsync();
            return carts;
            
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods()
        {
            var paymentMethods = await context.PaymentMethods.ToListAsync();
            return paymentMethods;
        }

        public async Task<int> InsertPayment(Payment payment)
        {
            context.Payments.Add(payment);
            var result = await context.SaveChangesAsync();
            if (result == 0) { return 0; }
            return 1;
        }

        public async Task<int> InsertOrder(Order order)
        {
            context.Orders.Add(order);
            var result = await context.SaveChangesAsync();
            if (result == 0) { return 0; }
            return 1;
        }
    }
}
