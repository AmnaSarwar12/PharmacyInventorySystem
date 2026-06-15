using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
namespace PharmacyInventorySystem.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    
    public class SaleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SaleController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
       public IActionResult GetSales()
        {
            var sales = _context.Sales.ToList();
            return Ok(sales);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSale([FromBody] Sale sale)
        {
            if (sale == null)
            {
                return BadRequest();
            }

            _context.Sales.Add(sale);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSales), new { id = sale.SaleID }, sale);
        }

        [HttpGet("{id}")]
        public IActionResult GetSale(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            var sale = _context.Sales.Find(id);
            if(sale == null)
            {
                return NotFound();
            }
            _context.Sales.Remove(sale);
            _context.SaveChanges();

            return NoContent();   
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSale(int id, [FromBody] Sale sale)
        {
            if (sale == null || sale.SaleID != id)
            {
                return BadRequest();
            }

            var existingSale = _context.Sales.Find(id);
            if (existingSale == null)
            {
                return NotFound();
            }

            existingSale.SaleID = sale.SaleID;
            existingSale.InvoiceID = sale.InvoiceID;
            existingSale.Medicine = sale.Medicine;
            existingSale.Quantity = sale.Quantity;
            existingSale.Amount = sale.Amount;
            existingSale.SaleDate = sale.SaleDate;

            _context.Sales.Update(existingSale);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchSales(string Medicine)
        {
           var sales = _context.Sales.Where(s => s.Medicine.ToLower().Contains(Medicine.ToLower())).ToList();
            if (!sales.Any())
            {
                return NotFound();
            }
            return Ok(sales);
        }

        [HttpGet("Count")]
        public IActionResult GetSaleCount()
        {
            var count = new
            {
                totalSales = _context.Sales.Count(),
                todaySales = _context.Sales.Where(s => s.SaleDate.Date == DateTime.Today).Sum(s => s.Amount),
                MonthSales = _context.Sales.Where(s => s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year).Sum(s => s.Amount)
            };
            return Ok(count);
        }
       
        
    }
}