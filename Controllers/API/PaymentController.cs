using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
namespace PharmacyInventorySystem.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
       public IActionResult GetPayments()
        {
            var payments = _context.Payments.ToList();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatePayment([FromBody] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetPayments), new { id = payment.Id }, payment);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] Payment payment)
        {
            var PaymentToUpdate = _context.Payments.Find(id);
            if(PaymentToUpdate == null)
            {
                return NotFound();
            }
            PaymentToUpdate.Supplier = payment.Supplier;
            PaymentToUpdate.Amount = payment.Amount;
            PaymentToUpdate.DueDate = payment.DueDate;
            PaymentToUpdate.Status = payment.Status;
            _context.Payments.Update(PaymentToUpdate);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("count")]
        public IActionResult GetPaymentStats()
        {
            var stat = new
            {
                totalPayment = _context.Payments.Sum(p => p.Amount),
                pendingPayment = _context.Payments.Where(p => p.Status == "Pending").Sum(p => p.Amount),
                invoices = _context.Payments.Count()
            };
            return Ok(stat);
        }
        
    }
}