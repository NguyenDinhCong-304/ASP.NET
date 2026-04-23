using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;
using NguyenDinhCong_2122110566.Enums;

[Route("api/admin/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL PRODUCTS (ADMIN)
    // =========================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return Ok(products);
    }

    // =========================
    // GET BY ID + ATTRIBUTE
    // =========================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var product = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Attributes)
                .ThenInclude(a => a.Attribute)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound(new { message = "Không tìm thấy sản phẩm" });

        return Ok(product);
    }

    // =========================
    // CREATE PRODUCT
    // =========================
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        product.CreatedAt = DateTime.UtcNow;

        if (string.IsNullOrEmpty(product.Slug))
        {
            product.Slug = product.Name
                .ToLower()
                .Trim()
                .Replace(" ", "-");
        }

        product.Status = ProductStatus.Active;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Tạo sản phẩm thành công",
            product.Id
        });
    }

    // =========================
    // UPDATE PRODUCT
    // =========================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Product updatedProduct)
    {
        if (id != updatedProduct.Id)
            return BadRequest(new { message = "ID không khớp" });

        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound(new { message = "Không tìm thấy sản phẩm" });

        product.BrandId = updatedProduct.BrandId;
        product.CategoryId = updatedProduct.CategoryId;
        product.Name = updatedProduct.Name;
        product.Slug = updatedProduct.Slug;
        product.Thumbnail = updatedProduct.Thumbnail;
        product.Content = updatedProduct.Content;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.Status = updatedProduct.Status;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật sản phẩm thành công" });
    }

    // =========================
    // DELETE PRODUCT
    // =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var product = await _context.Products
            .Include(p => p.Attributes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound(new { message = "Không tìm thấy sản phẩm" });

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa sản phẩm thành công" });
    }

    // =========================
    // CHANGE STATUS
    // =========================
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, [FromBody] ProductStatus status)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound(new { message = "Không tìm thấy sản phẩm" });

        product.Status = status;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Cập nhật trạng thái thành công",
            status
        });
    }

    // ==================================================
    // ATTRIBUTE VALUE (MỨC 1 - XỬ LÝ TRONG PRODUCT)
    // ==================================================

    // ADD ATTRIBUTE VALUE
    [HttpPost("{productId}/attributes")]
    public async Task<IActionResult> AddAttributeValue(long productId, ProductAttributeValue model)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
            return NotFound(new { message = "Không tìm thấy sản phẩm" });

        model.ProductId = productId;

        _context.ProductAttributeValues.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Thêm thuộc tính thành công",
            model
        });
    }

    // DELETE ATTRIBUTE VALUE
    [HttpDelete("{productId}/attributes/{id}")]
    public async Task<IActionResult> DeleteAttributeValue(long productId, long id)
    {
        var value = await _context.ProductAttributeValues
            .FirstOrDefaultAsync(x => x.Id == id && x.ProductId == productId);

        if (value == null)
            return NotFound(new { message = "Không tìm thấy thuộc tính" });

        _context.ProductAttributeValues.Remove(value);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa thuộc tính thành công" });
    }
}