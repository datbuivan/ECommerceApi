using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PaymentMethod>().Property(p=>p.Id).ValueGeneratedNever();
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Payment)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.PaymentId)
                .OnDelete(DeleteBehavior.ClientNoAction);
            modelBuilder.Entity<Order>()
                .HasOne(e => e.User)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
        public DbSet<Offer> Offers { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<PaymentMethod> PaymentMethods { set; get; }
        public DbSet<Payment> Payments { set; get; }
        public DbSet<Cart> Carts { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<CartItems> CartItems { set; get; }
        public DbSet<Review> Reviews { set; get; }

    }
}
