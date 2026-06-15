using Microsoft.AspNetCore.Mvc;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
using Microsoft.AspNetCore.Authorization;
namespace PharmacyInventorySystem.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrders), new { id = order.OrderID }, order);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if(order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (order == null || order.OrderID != id)
            {
                return BadRequest();
            }

            var existingOrder = _context.Orders.Find(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.OrderID = order.OrderID;
            existingOrder.Supplier = order.Supplier;
            existingOrder.Items = order.Items;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;

            _context.Orders.Update(existingOrder);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("count")]
        public IActionResult GetOrderCount()
        {
            var count = new
            {
                TotalOrders = _context.Orders.Count(),
                PendingOrders = _context.Orders.Count(o => o.Status == "Pending"),
                ReceivedOrders = _context.Orders.Count(o => o.Status == "Received")
            };
            return Ok(count);
        }

       
    }
}