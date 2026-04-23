using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;
using NguyenDinhCong_2122110566.Enums;

[Route("api/admin/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostController(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL POSTS
    // =========================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _context.Posts
            .Include(p => p.Topic)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return Ok(posts);
    }

    // =========================
    // GET BY ID
    // =========================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var post = await _context.Posts
            .Include(p => p.Topic)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
            return NotFound(new { message = "Không tìm thấy bài viết" });

        return Ok(post);
    }

    // =========================
    // CREATE POST
    // =========================
    [HttpPost]
    public async Task<IActionResult> Create(Post post)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        post.CreatedAt = DateTime.UtcNow;
        post.UpdatedAt = DateTime.UtcNow;

        // auto slug nếu chưa có
        if (string.IsNullOrEmpty(post.Slug))
        {
            post.Slug = post.Title.ToLower().Replace(" ", "-");
        }

        post.Status = PostStatus.Active;

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Tạo bài viết thành công",
            post.Id
        });
    }

    // =========================
    // UPDATE POST
    // =========================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Post updatedPost)
    {
        if (id != updatedPost.Id)
            return BadRequest(new { message = "ID không khớp" });

        var post = await _context.Posts.FindAsync(id);

        if (post == null)
            return NotFound(new { message = "Không tìm thấy bài viết" });

        post.TopicId = updatedPost.TopicId;
        post.Title = updatedPost.Title;
        post.Slug = updatedPost.Slug;
        post.Image = updatedPost.Image;
        post.Content = updatedPost.Content;
        post.Description = updatedPost.Description;
        post.Status = updatedPost.Status;

        post.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Cập nhật bài viết thành công"
        });
    }

    // =========================
    // DELETE POST
    // =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
            return NotFound(new { message = "Không tìm thấy bài viết" });

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa bài viết thành công" });
    }

    // =========================
    // CHANGE STATUS (ADMIN)
    // =========================
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] PostStatus status)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
            return NotFound(new { message = "Không tìm thấy bài viết" });

        post.Status = status;
        post.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Cập nhật trạng thái thành công",
            status
        });
    }
}