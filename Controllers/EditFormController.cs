using Microsoft.AspNetCore.Mvc;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
namespace PharmacyInventorySystem.Controllers
{
    public class EditFormController : Controller
    {
        private readonly AppDbContext _context;

        public EditFormController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult EditProductForm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditOrderForm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditSaleForm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditPaymentForm()
        {
            return View();
        }

    }
}