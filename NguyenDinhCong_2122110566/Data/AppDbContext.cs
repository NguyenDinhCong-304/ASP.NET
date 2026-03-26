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
    }
}
