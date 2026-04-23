using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Enums;
using NguyenDinhCong_2122110566.Models;

[Route("api/admin/[controller]")]
[ApiController]
public class ProductSaleController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductSaleController(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL SALES
    // =========================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _context.ProductSales
            .Include(x => x.Product)
            .OrderByDescending(x => x.DateBegin)
            .ToListAsync();

        return Ok(sales);
    }

    // =========================
    // GET BY ID
    // =========================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var sale = await _context.ProductSales
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (sale == null)
            return NotFound(new { message = "Không tìm thấy sale" });

        return Ok(sale);
    }

    // =========================
    // CREATE SALE
    // =========================
    [HttpPost]
    public async Task<IActionResult> Create(ProductSale model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (model.DateEnd < model.DateBegin)
            return BadRequest(new { message = "Ngày kết thúc phải lớn hơn ngày bắt đầu" });

        _context.ProductSales.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Tạo sale thành công",
            model.Id
        });
    }

    // =========================
    // UPDATE SALE
    // =========================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, ProductSale updated)
    {
        if (id != updated.Id)
            return BadRequest(new { message = "ID không khớp" });

        var sale = await _context.ProductSales.FindAsync(id);

        if (sale == null)
            return NotFound(new { message = "Không tìm thấy sale" });

        if (updated.DateEnd < updated.DateBegin)
            return BadRequest(new { message = "Ngày kết thúc không hợp lệ" });

        sale.ProductId = updated.ProductId;
        sale.PriceSale = updated.PriceSale;
        sale.DateBegin = updated.DateBegin;
        sale.DateEnd = updated.DateEnd;
        sale.Status = updated.Status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật sale thành công" });
    }

    // =========================
    // DELETE SALE
    // =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var sale = await _context.ProductSales.FindAsync(id);

        if (sale == null)
            return NotFound(new { message = "Không tìm thấy sale" });

        _context.ProductSales.Remove(sale);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa sale thành công" });
    }

    // =========================
    // CHANGE STATUS
    // =========================
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] ProductSaleStatus status)
    {
        var sale = await _context.ProductSales.FindAsync(id);

        if (sale == null)
            return NotFound(new { message = "Không tìm thấy sale" });

        sale.Status = status;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Cập nhật trạng thái thành công",
            status
        });
    }

    // =========================
    // GET ACTIVE SALES
    // =========================
    //[HttpGet("active")]
    //public async Task<IActionResult> GetActiveSales()
    //{
    //    var now = DateTime.UtcNow;

    //    var sales = await _context.ProductSales
    //        .Include(x => x.Product)
    //        .Where(x =>
    //            x.Status == 1 &&
    //            x.DateBegin <= now &&
    //            x.DateEnd >= now)
    //        .ToListAsync();

    //    return Ok(sales);
    //}
}