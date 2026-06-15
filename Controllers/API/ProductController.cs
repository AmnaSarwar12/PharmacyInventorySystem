using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
namespace PharmacyInventorySystem.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            return BadRequest(ModelState);
        }

      [HttpGet("Search")]
      public IActionResult SearchProducts(string name)
        {
            var products = _context.Products.Where(p=>p.Name.ToLower().Contains(name.ToLower())).ToList();
            if(!products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        return NoContent();
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        //find existing product
        var ProductToUpdate = _context.Products.Find(id);
        if (ProductToUpdate == null)
        {
            return NotFound();
        }
        //update properties
        ProductToUpdate.Name = product.Name;
        ProductToUpdate.Category = product.Category;
        ProductToUpdate.Quantity = product.Quantity;
        ProductToUpdate.ExpiryDate = product.ExpiryDate;
        _context.Products.Update(ProductToUpdate);
        _context.SaveChanges();
        return Ok(ProductToUpdate);
    }

    [HttpGet("counts")]
    public IActionResult GetCounts()
        {
            var counts = new
            {
                totalItems = _context.Products.Count(),
                outOfStock = _context.Products.Count(p => p.Quantity == 0),
                expiringSoon = _context.Products.Count(p => p.ExpiryDate <= DateTime.Now.AddDays(30) && p.ExpiryDate > DateTime.Now),
                lowStock = _context.Products.Count(p => p.Quantity > 0 && p.Quantity <= 5)

            };
            return Ok(counts);
        }

    }
}