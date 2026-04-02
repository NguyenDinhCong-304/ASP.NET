using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhCong_2122110566.Data;
using NguyenDinhCong_2122110566.Models;
using System.Collections.Generic;
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
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updated)
        {
            if (id != updated.Id) return BadRequest("Id in URL must match Id in body.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            // Update allowed fields explicitly
            product.Name = updated.Name;
            product.Description = updated.Description;
            product.Price = updated.Price;
            product.Stock = updated.Stock;
            product.CategoryId = updated.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Products.AnyAsync(p => p.Id == id))
                    return NotFound();

                throw;
            }
        }

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