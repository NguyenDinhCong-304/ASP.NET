using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenDinhCong_2122110566.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/Products
        //[HttpPost]
        //public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    // Validate Category
        //    var categoryExists = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);
        //    if (!categoryExists) return BadRequest(new { CategoryId = "Không tìm thấy danh mục." });

        //    // Validate Brand if provided
        //    if (product.BrandId.HasValue)
        //    {
        //        var brandExists = await _context.Set<Brand>().AnyAsync(b => b.Id == product.BrandId.Value);
        //        if (!brandExists) return BadRequest(new { BrandId = "Không tìm thấy thương hiệu." });
        //    }

        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        //}

        // PUT: api/Products/5
        // Expects the client to provide current RowVersion for optimistic concurrency (byte[] -> base64 in JSON)
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updated)
        //{
        //    if (id != updated.Id) return BadRequest("ID trong URL và form không trùng khớp.");
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null) return NotFound();

        //    // Validate Category
        //    var categoryExists = await _context.Categories.AnyAsync(c => c.Id == updated.CategoryId);
        //    if (!categoryExists) return BadRequest(new { CategoryId = "Không tìm thấy danh mục." });

        //    // Validate Brand if provided
        //    if (updated.BrandId.HasValue)
        //    {
        //        var brandExists = await _context.Set<Brand>().AnyAsync(b => b.Id == updated.BrandId.Value);
        //        if (!brandExists) return BadRequest(new { BrandId = "Không tìm thấy thương hiệu." });
        //    }

        //    // Set original RowVersion for concurrency check (client must send RowVersion)
        //    if (updated.RowVersion != null)
        //    {
        //        _context.Entry(product).Property(p => p.RowVersion).OriginalValue = updated.RowVersion;
        //    }

        //    // Update allowed fields explicitly
        //    product.Name = updated.Name;
        //    product.Description = updated.Description;
        //    product.Price = updated.Price;
        //    product.Stock = updated.Stock;
        //    product.CategoryId = updated.CategoryId;
        //    product.BrandId = updated.BrandId;
        //    product.ImageUrl = updated.ImageUrl;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return NoContent();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        // If product no longer exists
        //        if (!await _context.Products.AnyAsync(p => p.Id == id))
        //            return NotFound();

        //        return Conflict(new { message = "Sản phẩm đã được sửa bởi người khác. Tải và thử lại." });
        //    }
        //}

        // DELETE: api/Products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}