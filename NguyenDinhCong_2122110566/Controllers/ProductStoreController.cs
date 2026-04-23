using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;

[Route("api/admin/[controller]")]
[ApiController]
public class ProductStoreController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductStoreController(AppDbContext context)
    {
        _context = context;
    }

    // =====================
    // GET ALL STOCK
    // =====================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.ProductStores
            .Include(x => x.Product)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(data);
    }

    // =====================
    // GET BY PRODUCT
    // =====================
    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProduct(long productId)
    {
        var data = await _context.ProductStores
            .Where(x => x.ProductId == productId)
            .Include(x => x.Product)
            .ToListAsync();

        return Ok(data);
    }

    // =====================
    // IMPORT STOCK
    // =====================
    [HttpPost]
    public async Task<IActionResult> Import(ProductStore model)
    {
        var product = await _context.Products.FindAsync(model.ProductId);

        if (product == null)
            return NotFound(new { message = "Không tìm thấy sản phẩm" });

        model.CreatedAt = DateTime.UtcNow;

        _context.ProductStores.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Nhập kho thành công",
            model.Id
        });
    }

    // =====================
    // UPDATE STOCK
    // =====================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, ProductStore updated)
    {
        var store = await _context.ProductStores.FindAsync(id);

        if (store == null)
            return NotFound();

        store.PriceRoot = updated.PriceRoot;
        store.Qty = updated.Qty;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật kho thành công" });
    }

    // =====================
    // DELETE STOCK RECORD
    // =====================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var store = await _context.ProductStores.FindAsync(id);

        if (store == null)
            return NotFound();

        _context.ProductStores.Remove(store);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa kho thành công" });
    }
}