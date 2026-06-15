using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
namespace PharmacyInventorySystem.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    
    public class SupplierController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupplierController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
        public IActionResult GetSuppliers()
        {
            var suppliers = _context.Suppliers.ToList();
            return Ok(suppliers);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSupplier([FromBody] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetSuppliers), new { id = supplier.Id }, supplier);
            }
            return BadRequest(ModelState);
        }

        
      

      
       

    }
}