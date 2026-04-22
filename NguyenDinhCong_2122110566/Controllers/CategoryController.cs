using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

namespace NguyenDinhCong_2122110566.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // ===================================
        // GET: api/category
        // Lấy tất cả danh mục
        // ===================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories
                .OrderBy(c => c.SortOrder)
                .ToListAsync();
        }

        // ===================================
        // GET: api/category/5
        // Lấy danh mục theo ID
        // ===================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(long id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // ===================================
        // POST: api/category
        // Thêm danh mục
        // ===================================
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            category.CreatedAt = DateTime.Now;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // ===================================
        // PUT: api/category/5
        // Cập nhật danh mục
        // ===================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(long id, Category category)
        {
            var cat = await _context.Categories.FindAsync(id);

            if (cat == null)
                return NotFound();

            cat.Name = category.Name;
            cat.Slug = category.Slug;
            cat.Image = category.Image;
            cat.ParentId = category.ParentId;
            cat.SortOrder = category.SortOrder;
            cat.Description = category.Description;
            cat.Status = category.Status;

            await _context.SaveChangesAsync();

            return Ok(cat);
        }

        // ===================================
        // DELETE: api/category/5
        // Xóa danh mục
        // ===================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ===================================
        // GET: api/category/parent/0
        // Lấy danh mục theo parent
        // ===================================
        [HttpGet("parent/{parentId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryByParent(long parentId)
        {
            return await _context.Categories
                .Where(c => c.ParentId == parentId)
                .OrderBy(c => c.SortOrder)
                .ToListAsync();
        }
    }
}