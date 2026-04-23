using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

[Route("api/admin/[controller]")]
[ApiController]
public class TopicController : ControllerBase
{
    private readonly AppDbContext _context;

    public TopicController(AppDbContext context)
    {
        _context = context;
    }

    // =====================
    // GET ALL TOPICS
    // =====================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var topics = await _context.Topics
            .Include(x => x.Posts)
            .OrderBy(x => x.SortOrder)
            .ToListAsync();

        return Ok(topics);
    }

    // =====================
    // GET BY ID
    // =====================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var topic = await _context.Topics
            .Include(x => x.Posts)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (topic == null)
            return NotFound(new { message = "Không tìm thấy topic" });

        return Ok(topic);
    }

    // =====================
    // CREATE
    // =====================
    [HttpPost]
    public async Task<IActionResult> Create(Topic model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        model.CreatedAt = DateTime.UtcNow;

        _context.Topics.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Tạo topic thành công",
            model.Id
        });
    }

    // =====================
    // UPDATE
    // =====================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Topic updated)
    {
        if (id != updated.Id)
            return BadRequest(new { message = "ID không khớp" });

        var topic = await _context.Topics.FindAsync(id);

        if (topic == null)
            return NotFound(new { message = "Không tìm thấy topic" });

        topic.Name = updated.Name;
        topic.Slug = updated.Slug;
        topic.SortOrder = updated.SortOrder;
        topic.Description = updated.Description;
        topic.Status = updated.Status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật thành công" });
    }

    // =====================
    // DELETE
    // =====================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var topic = await _context.Topics.FindAsync(id);

        if (topic == null)
            return NotFound(new { message = "Không tìm thấy topic" });

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa topic thành công" });
    }

    // =====================
    // CHANGE STATUS
    // =====================
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] int status)
    {
        var topic = await _context.Topics.FindAsync(id);

        if (topic == null)
            return NotFound(new { message = "Không tìm thấy topic" });

        topic.Status = status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật trạng thái thành công" });
    }
}