using ECommerceApi.Models;

namespace ECommerceApi.DataAccess
{
    public interface IDataAccess
    {
        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<ProductCategory> GetProductCategory(int? id);
        Task<Offer> GetOffer(int? id);
        Task<Product> GetProductById(int? id);
        Task<IEnumerable<Product>> GetProducts(string category, string subcategory, int count);
        Task<bool> AddUser(User user);
        string IsUserPresent(string email, string password);
        Task<bool> InsertReview(Review review);
        Task<IEnumerable<Review>> GetproductReview(int? productId);
        Task<User> GetUser(int? id);
        Task<bool> InserCartItem(int userid, int productid);
        Task<Cart> GetActiveCartOfUser(int? userid);
        Task<Cart> GetCart(int? cartid);
        Task<IEnumerable<Cart>> GetAllPreviousCartOfUser(int? userid);  
        Task<IEnumerable<PaymentMethod>> GetPaymentMethods();
        Task<int> InsertPayment(Payment payment);
        Task<int> InsertOrder(Order order);
    }
}
