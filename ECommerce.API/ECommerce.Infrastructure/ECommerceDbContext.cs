using ECommerce.API.ECommerce.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.ECommerce.Infrastructure
{
    public class ECommerceDbContext : IdentityDbContext<ApplicationUser>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        #region DbSet properties
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSizeColor> ProductSizeColors { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.Email).IsUnique();

            // Configure relationships
            //ConfigureCart(modelBuilder);
            //ConfigureWishlist(modelBuilder);
            ConfigureOrder(modelBuilder);
            ConfigureProduct(modelBuilder);
            ConfigureOrderDetails(modelBuilder);

            // Configure composite primary key for ProductSize entity
            modelBuilder.Entity<ProductSizeColor>()
                .HasKey(ps => new { ps.ProductId, ps.SizeName, ps.ColorName });
            //// Configure composite primary key for Wishlist entity
            modelBuilder.Entity<Cart>()
                .HasKey(c => new { c.ProductId, c.ApplicationUserId, c.Size, c.Color });
            //// Configure composite primary key for Cart entity
            modelBuilder.Entity<Wishlist>()
                .HasKey(w => new { w.ApplicationUserId, w.ProductId });
            // Make ProductId in OrderDetails unique
            modelBuilder.Entity<OrderDetails>()
                .HasIndex(od => od.ProductId).IsUnique();

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Order>()
                 .HasOne(o => o.Address)
                 .WithMany()
                 .HasForeignKey(o => o.AddressId);

            modelBuilder.Entity<Cart>()
                .HasIndex(c => new { c.Size, c.Color })
                .IsUnique();
        }
        #region Relationships
        //private void ConfigureCart(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Cart>()
        //        .HasOne(c => c.ApplicationUser)
        //        .WithMany(u => u.Carts)
        //        .HasForeignKey(c => c.ApplicationUserId)
        //        .IsRequired();
        //}

        //private void ConfigureWishlist(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Wishlist>()
        //        .HasOne(w => w.ApplicationUser)
        //        .WithMany(u => u.Wishlists)
        //        .HasForeignKey(w => w.ApplicationUserId)
        //        .IsRequired();
        //}

        private void ConfigureOrder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ApplicationUserId)
                .IsRequired();
        }

        private void ConfigureProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryName)
                .IsRequired();
        }

        private void ConfigureOrderDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .IsRequired();

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .IsRequired();
        }
        #endregion
    }
}