using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

[Route("api/admin/[controller]")]
[ApiController]
public class SettingController : ControllerBase
{
    private readonly AppDbContext _context;

    public SettingController(AppDbContext context)
    {
        _context = context;
    }

    // =====================
    // GET ALL SETTINGS
    // =====================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var settings = await _context.Settings.ToListAsync();
        return Ok(settings);
    }

    // =====================
    // GET BY ID
    // =====================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var setting = await _context.Settings.FindAsync(id);

        if (setting == null)
            return NotFound(new { message = "Không tìm thấy setting" });

        return Ok(setting);
    }

    // =====================
    // CREATE
    // =====================
    [HttpPost]
    public async Task<IActionResult> Create(Setting model)
    {
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;

        _context.Settings.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Tạo setting thành công" });
    }

    // =====================
    // UPDATE
    // =====================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Setting updated)
    {
        var setting = await _context.Settings.FindAsync(id);

        if (setting == null)
            return NotFound(new { message = "Không tìm thấy setting" });

        setting.SiteName = updated.SiteName;
        setting.Email = updated.Email;
        setting.Phone = updated.Phone;
        setting.Address = updated.Address;
        setting.Status = updated.Status;
        setting.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật setting thành công" });
    }

    // =====================
    // DELETE
    // =====================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var setting = await _context.Settings.FindAsync(id);

        if (setting == null)
            return NotFound(new { message = "Không tìm thấy setting" });

        _context.Settings.Remove(setting);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa setting thành công" });
    }

    // =====================
    // CHANGE STATUS
    // =====================
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] int status)
    {
        var setting = await _context.Settings.FindAsync(id);

        if (setting == null)
            return NotFound(new { message = "Không tìm thấy setting" });

        setting.Status = status;
        setting.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật trạng thái thành công" });
    }
}