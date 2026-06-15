using Microsoft.AspNetCore.Mvc;
using PharmacyInventorySystem.Data;
namespace PharmacyInventorySystem.Controllers
{
    public class ChartController : Controller
    {
        private readonly AppDbContext _context;

        public ChartController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult DashboardCharts()
        {
            return View();
        }
        
        
    }
}