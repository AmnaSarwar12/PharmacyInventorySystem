using Microsoft.AspNetCore.Mvc;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;

namespace PharmacyInventorySystem.Controllers
{
    public class InventoryController : Controller
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public IActionResult Dashboard()
        {
            //for Total Items
             ViewBag.TotalItems = _context.Products.Count();

             //for low stock
                ViewBag.LowStock = ViewBag.LowStock = _context.Products.Count(p => p.Quantity > 0 && p.Quantity <= 10);
            //for out of stock
                ViewBag.OutOfStock = _context.Products.Count(p => p.Quantity == 0);

            //for expired items
                ViewBag.ExpiringSoon = _context.Products.Count(p => p.ExpiryDate <= DateTime.Now.AddDays(30) && p.ExpiryDate > DateTime.Now);

                var products = _context.Products.ToList();
                return View(products);

        }

        [HttpGet]
        public IActionResult ProductForm()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult ProductForm(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["ToastMessage"] = "Product added successfully.";
                TempData["ToastType"] = "success";
                Console.WriteLine("Product added: " + product.Name);
                return RedirectToAction("Dashboard","Inventory");
            }
            return View();
        }

        [HttpGet]
        public IActionResult SupplierDashboard()
        {
           var suppliers = _context.Suppliers.ToList();
           return View(suppliers);
        }

        [HttpGet]
        public IActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSupplier(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
                TempData["ToastMessage"] = "Supplier added successfully.";
                TempData["ToastType"] = "success";
                Console.WriteLine("Supplier added: " + supplier.Name);
                return RedirectToAction("SupplierDashboard", "Inventory");
            }
            return View();
        }

        [HttpGet]
        public IActionResult OrderDashboard()
        {
            ViewBag.TotalOrders = _context.Orders.Count();
            ViewBag.PendingOrders = _context.Orders.Count(o => o.Status == "Pending");
            ViewBag.ReceivedOrders = _context.Orders.Count(o => o.Status == "Received");
            var order = _context.Orders.ToList();
            return View(order);
            
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                TempData["ToastMessage"] = "Order added successfully.";
                TempData["ToastType"] = "success";
                Console.WriteLine("Order added: " + order.Supplier);
                return RedirectToAction("OrderDashboard", "Inventory");
            }
            return View();
        }

        [HttpGet]
        public IActionResult SaleDashboard()
        {
            var TodaySale = _context.Sales.Where(s => s.SaleDate.Date == DateTime.Today).ToList();
            ViewBag.TodaySaleCount = TodaySale.Sum(s => s.Amount);
            ViewBag.TotalSales = _context.Sales.Count();
            //total amount this month
            var ThisMonthSale = _context.Sales.Where(s => s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year).Sum(s => s.Amount);
            ViewBag.ThisMonthSale = ThisMonthSale;
            var sale = _context.Sales.ToList();
            return View(sale);
        
        }

        [HttpGet]
        public IActionResult PaymentDashboard()
        {
            ViewBag.TotalPayments = _context.Payments.Sum(p => p.Amount);
            ViewBag.invoiceCount = _context.Payments.Count();
            ViewBag.PendingPayments = _context.Payments.Where(p => p.Status == "Pending" || p.Status == "Overdue").Sum(p => p.Amount);
            var payments = _context.Payments.ToList();
            return View(payments);
           
        }
    }
}