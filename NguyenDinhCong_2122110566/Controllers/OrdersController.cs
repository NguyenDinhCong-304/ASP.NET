using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;
using NguyenDinhCong_2122110566.Enums;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAll()
    {
        var orders = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ToListAsync();

        return Ok(orders);
    }

    // GET: api/order/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(long id)
    {
        var order = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return NotFound(new { message = "Không tìm thấy đơn hàng" });

        return Ok(order);
    }

    // POST: api/order
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        order.CreatedAt = DateTime.UtcNow;
        order.Status = OrderStatus.Pending;

        // Quan trọng: đảm bảo relationship đúng
        if (order.OrderDetails != null)
        {
            foreach (var item in order.OrderDetails)
            {
                item.Order = order; // gán cha
            }
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Đặt hàng thành công",
            orderId = order.Id
        });
    }

    // PUT: api/order/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Order updatedOrder)
    {
        if (id != updatedOrder.Id)
            return BadRequest(new { message = "ID không khớp" });

        var order = await _context.Orders.FindAsync(id);
        if (order == null)
            return NotFound(new { message = "Không tìm thấy đơn hàng" });

        // cập nhật field
        order.Name = updatedOrder.Name;
        order.Email = updatedOrder.Email;
        order.Phone = updatedOrder.Phone;
        order.Address = updatedOrder.Address;
        order.Note = updatedOrder.Note;
        order.Status = updatedOrder.Status;

        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
            return NotFound(new { message = "Không tìm thấy đơn hàng" });

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa đơn hàng thành công" });
    }

    // PATCH: api/order/{id}/status
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
            return NotFound(new { message = "Không tìm thấy đơn hàng" });

        order.Status = status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật trạng thái thành công", status });
    }
   
}