using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrandController(AppDbContext context)
    {
        _context = context;
    }

    // =====================
    // GET ALL
    // =====================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _context.Brands
            .OrderBy(x => x.Name)
            .ToListAsync();

        return Ok(brands);
    }

    // =====================
    // GET BY ID
    // =====================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var brand = await _context.Brands
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (brand == null)
            return NotFound();

        return Ok(brand);
    }

    // =====================
    // CREATE
    // =====================
    [HttpPost]
    public async Task<IActionResult> Create(Brand model)
    {
        _context.Brands.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Tạo brand thành công" });
    }

    // =====================
    // UPDATE
    // =====================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Brand updated)
    {
        var brand = await _context.Brands.FindAsync(id);

        if (brand == null)
            return NotFound();

        brand.Name = updated.Name;
        brand.Slug = updated.Slug;
        brand.Logo = updated.Logo;
        brand.Description = updated.Description;
        brand.Country = updated.Country;
        brand.Status = updated.Status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật brand thành công" });
    }

    // =====================
    // DELETE
    // =====================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var brand = await _context.Brands.FindAsync(id);

        if (brand == null)
            return NotFound();

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa brand thành công" });
    }

    // =====================
    // CHANGE STATUS
    // =====================
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] int status)
    {
        var brand = await _context.Brands.FindAsync(id);

        if (brand == null)
            return NotFound();

        brand.Status = status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật status thành công" });
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadLogo([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/brand");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var path = Path.Combine(folder, fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var url = $"/images/brand/{fileName}";

        return Ok(new { url });
    }
}