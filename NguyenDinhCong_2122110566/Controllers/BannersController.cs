using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

namespace NguyenDinhCong_2122110566.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BannersController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET: api/banner
        // Lấy danh sách banner
        // =========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banner>>> GetBanners()
        {
            return await _context.Banners
                .OrderBy(b => b.SortOrder)
                .ToListAsync();
        }

        // =========================
        // GET: api/banner/5
        // Lấy banner theo id
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Banner>> GetBanner(long id)
        {
            var banner = await _context.Banners.FindAsync(id);

            if (banner == null)
            {
                return NotFound();
            }

            return banner;
        }

        // =========================
        // POST: api/banner
        // Thêm banner
        // =========================
        [HttpPost]
        public async Task<ActionResult<Banner>> CreateBanner(Banner banner)
        {
            banner.CreatedAt = DateTime.Now;
            banner.UpdatedAt = DateTime.Now;

            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBanner), new { id = banner.Id }, banner);
        }

        // =========================
        // PUT: api/banner/5
        // Cập nhật banner
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(long id, Banner banner)
        {
            if (id != banner.Id)
            {
                return BadRequest();
            }

            banner.UpdatedAt = DateTime.Now;

            _context.Entry(banner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Banners.Any(e => e.Id == id))
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

        // =========================
        // DELETE: api/banner/5
        // Xóa banner
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(long id)
        {
            var banner = await _context.Banners.FindAsync(id);

            if (banner == null)
            {
                return NotFound();
            }

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
