using Microsoft.AspNetCore.Mvc;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;
namespace PharmacyInventorySystem.Controllers
{
    public class FormController : Controller
    {
        private readonly AppDbContext _context;

        public FormController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult NewSale()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewSale(Sale sale)
        {
            if (ModelState.IsValid)
            {
                var selectedDate = Request.Form["selectedDate"];
                sale.SaleDate = string.IsNullOrEmpty(selectedDate) ? DateTime.Now : DateTime.Parse(selectedDate);
                _context.Sales.Add(sale);
                _context.SaveChanges();
                TempData["ToastMessage"] = "Sale added successfully.";
                TempData["ToastType"] = "success";
                Console.WriteLine("Sale added: " + sale.Medicine);
                return RedirectToAction("SaleDashboard", "Inventory");
            }
            return View();
        }

        [HttpGet]
        public IActionResult NewPayment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
                TempData["ToastMessage"] = "Payment added successfully.";
                TempData["ToastType"] = "success";
                Console.WriteLine("Payment added: " + payment.Supplier);
                return RedirectToAction("PaymentDashboard", "Inventory");
            }
            return View();
        }
    }

        
}