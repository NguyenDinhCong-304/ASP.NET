using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

[Route("api/admin/attribute")]
[ApiController]
public class ProductAttributeController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductAttributeController(AppDbContext context)
    {
        _context = context;
    }

    // =====================
    // GET ALL
    // =====================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var attrs = await _context.ProductAttributes
            .Include(a => a.Values)
            .ToListAsync();

        return Ok(attrs);
    }

    // =====================
    // CREATE ATTRIBUTE
    // =====================
    [HttpPost]
    public async Task<IActionResult> Create(ProductAttribute model)
    {
        _context.ProductAttributes.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Tạo thuộc tính thành công",
            model.Id
        });
    }

    // =====================
    // DELETE ATTRIBUTE
    // =====================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var attr = await _context.ProductAttributes.FindAsync(id);

        if (attr == null)
            return NotFound();

        _context.ProductAttributes.Remove(attr);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa thành công" });
    }
}