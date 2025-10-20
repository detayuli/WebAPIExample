using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIExample.Data;
using WebAPIExample.Data.Models;

namespace WebAPIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // =============================
        // 🔹 GET ALL PRODUCTS
        // =============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return Ok(products);
        }

        // =============================
        // 🔹 GET PRODUCT BY ID
        // =============================
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                                                 .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

        // =============================
        // 🔹 CREATE PRODUCT
        // =============================
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product newProduct)
        {
            if (newProduct == null)
                return BadRequest("Product data is required.");

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        // =============================
        // 🔹 UPDATE PRODUCT
        // =============================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest("Product ID mismatch.");

            _context.Entry(updatedProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =============================
        // 🔹 DELETE PRODUCT
        // =============================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // 🔹 Cek apakah masih ada data
            var maxId = await _context.Products.AnyAsync()
                ? await _context.Products.MaxAsync(p => p.Id)
                : 0;

            // 🔹 Reset identity (agar bisa pakai ID kosong lagi)
            await _context.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT ('Products', RESEED, {maxId})");

            return NoContent();
        }

        // =============================
        // 🔹 CATEGORY ENDPOINTS
        // =============================
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            return Ok(category);
        }
    }

}
