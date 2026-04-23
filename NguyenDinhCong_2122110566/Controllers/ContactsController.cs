using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

namespace NguyenDinhCong_2122110566.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        // =============================
        // GET: api/contact
        // Lấy tất cả contact
        // =============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts
                .Include(c => c.User)
                .ToListAsync();
        }

        // =============================
        // GET: api/contact/5
        // Lấy contact theo id
        // =============================
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(long id)
        {
            var contact = await _context.Contacts
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
                return NotFound();

            return contact;
        }

        // =============================
        // GET: api/contact/user/1
        // Lấy contact theo user
        // =============================
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContactByUser(long userId)
        {
            return await _context.Contacts
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        // =============================
        // POST: api/contact
        // Tạo contact
        // =============================
        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        // =============================
        // PUT: api/contact/5
        // Cập nhật contact
        // =============================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(long id, Contact model)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                return NotFound();

            contact.Name = model.Name;
            contact.Email = model.Email;
            contact.Phone = model.Phone;
            contact.Content = model.Content;
            contact.ReplyId = model.ReplyId;
            contact.Status = model.Status;

            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        // =============================
        // DELETE: api/contact/5
        // Xóa contact
        // =============================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(long id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                return NotFound();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}