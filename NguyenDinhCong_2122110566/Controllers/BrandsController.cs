using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

namespace NguyenDinhCong_2122110566.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        // ===============================
        // GET: api/brand
        // Lấy tất cả thương hiệu
        // ===============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        // ===============================
        // GET: api/brand/5
        // Lấy brand theo id
        // ===============================
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(long id)
        {
            var brand = await _context.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // ===============================
        // POST: api/brand
        // Thêm brand
        // ===============================
        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }

        // ===============================
        // PUT: api/brand/5
        // Cập nhật brand
        // ===============================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(long id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Brands.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ===============================
        // DELETE: api/brand/5
        // Xóa brand
        // ===============================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(long id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}