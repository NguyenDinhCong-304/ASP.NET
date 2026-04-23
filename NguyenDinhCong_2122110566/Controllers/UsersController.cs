using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.DTO;
using NguyenDinhCong_2122110566.Models;

[Route("api/admin/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // =====================
    // GET ALL USERS
    // =====================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _context.Users
            .Include(u => u.Orders)
            .ToListAsync();

        return Ok(users);
    }

    // =====================
    // GET BY ID
    // =====================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _context.Users
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // =====================
    // CREATE USER
    // =====================
    [HttpPost]
    public async Task<IActionResult> Create(UserCreateDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Username = dto.Username,
            Roles = dto.Roles,
            Status = 1,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Tạo user thành công" });
    }

    // =====================
    // UPDATE USER
    // =====================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, User updated)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        user.Name = updated.Name;
        user.Email = updated.Email;
        user.Phone = updated.Phone;
        user.Username = updated.Username;
        user.Roles = updated.Roles;
        user.Status = updated.Status;
        user.Avatar = updated.Avatar;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật user thành công" });
    }

    // =====================
    // DELETE USER
    // =====================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa user thành công" });
    }

    // =====================
    // CHANGE STATUS
    // =====================
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] int status)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        user.Status = status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật trạng thái thành công" });
    }
}