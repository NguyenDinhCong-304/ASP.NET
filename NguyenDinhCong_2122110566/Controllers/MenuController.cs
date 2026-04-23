using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

namespace NguyenDinhCong_2122110566.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        // ===========================
        // GET: api/menu
        // Lấy tất cả menu
        // ===========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _context.Menus
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }

        // ===========================
        // GET: api/menu/5
        // Lấy menu theo id
        // ===========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(long id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
                return NotFound();

            return menu;
        }

        // ===========================
        // POST: api/menu
        // Thêm menu
        // ===========================
        [HttpPost]
        public async Task<ActionResult<Menu>> CreateMenu(Menu menu)
        {
            menu.CreatedAt = DateTime.Now;

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenu), new { id = menu.Id }, menu);
        }

        // ===========================
        // PUT: api/menu/5
        // Cập nhật menu
        // ===========================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(long id, Menu model)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
                return NotFound();

            menu.Name = model.Name;
            menu.Link = model.Link;
            menu.Type = model.Type;
            menu.ParentId = model.ParentId;
            menu.SortOrder = model.SortOrder;
            menu.TableId = model.TableId;
            menu.Status = model.Status;

            await _context.SaveChangesAsync();

            return Ok(menu);
        }

        // ===========================
        // DELETE: api/menu/5
        // Xóa menu
        // ===========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(long id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
                return NotFound();

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ===========================
        // GET: api/menu/parent/0
        // Lấy menu cha
        // ===========================
        [HttpGet("parent/{parentId}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetByParent(long parentId)
        {
            return await _context.Menus
                .Where(m => m.ParentId == parentId)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }
    }
}