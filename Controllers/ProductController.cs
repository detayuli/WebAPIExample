using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPIExample;

namespace WebAPIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // =============================
        // 🔹 MOCK DATA (sementara)
        // =============================
        private static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Accessories" },
            new Category { Id = 3, Name = "Home Appliances" }
        };

        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop", Price = 999.99m, Category = new Category { Id = 1, Name = "Electronics" } },
            new Product { Id = 2, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m, Category = new Category { Id = 2, Name = "Accessories" } }
        };

        // =============================
        // 🔹 CREATE (POST)
        // =============================
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
                return BadRequest("Product data is required.");

            // Pastikan ID tidak diperlukan dari client
            newProduct.Id = new Random().Next(100, 999);

            products.Add(newProduct);

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }


        // =============================
        // 🔹 READ (GET ALL)
        // =============================
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

        // =============================
        // 🔹 UPDATE (PUT)
        // =============================
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest("Product ID mismatch.");

            var existingProduct = products.Find(p => p.Id == id);
            if (existingProduct == null)
                return NotFound($"Product with ID {id} not found.");

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Category = updatedProduct.Category;
            // Logic to update the product (e.g., database call)
            return NoContent(); // Indicates the update was successful but no content is returned
        }

        // =============================
        // 🔹 DELETE
        // =============================
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            products.Remove(product);
            return NoContent(); 
        }

        // =============================
        // 🔹 CATEGORY ENDPOINTS
        // =============================
        [HttpGet("category")]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            return Ok(categories);
        }

        [HttpGet("category/{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = categories.Find(c => c.Id == id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            return Ok(category);
        }
    }
}
