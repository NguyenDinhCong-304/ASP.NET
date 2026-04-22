using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Models;

namespace NguyenDinhCong_2122110566.Data
{
    public class AppDbContext: DbContext
    {
        // Constructor này bắt buộc phải có để nhận Connection String từ Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductSale> ProductSales { get; set; }

        public DbSet<ProductStore> ProductStores { get; set; }

        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        public DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Banner> Banners { get; set; }
    }
}
