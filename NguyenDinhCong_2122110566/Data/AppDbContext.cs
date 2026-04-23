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

        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<ProductAttributeValue>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Attributes)
                .HasForeignKey(v => v.ProductId);

            modelBuilder.Entity<ProductAttributeValue>()
                .HasOne(v => v.Attribute)
                .WithMany(a => a.Values)
                .HasForeignKey(v => v.AttributeId);
        }
    }
}
