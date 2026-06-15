using Microsoft.AspNetCore.Mvc;
using PharmacyInventorySystem.Data;
using PharmacyInventorySystem.Models;

namespace PharmacyInventorySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]//post user registration data
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var ExistingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if(ExistingUser != null)
                {
                    TempData["ToastMessage"] = "Username already exists. Please choose a different username.";
                    TempData["ToastType"] = "danger";
                    return View("Register");
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["ToastMessage"] = "Registration successful. Please login.";
                TempData["ToastType"] = "success";
                return RedirectToAction("Login","Account");
            }
            return View(user);
        }

            [HttpGet]
            public IActionResult Login()
            {
                return View();  
            }

        [HttpPost]//post user login data
        public IActionResult Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == loginDto.Username);
            if (user == null)
            {
                TempData["ToastMessage"] = "Invalid username. Please try again.";
                TempData["ToastType"] = "danger";
                return View("Login");
            }
            if (user.Password != loginDto.Password)
            {
                TempData["ToastMessage"] = "Invalid password. Please try again.";
                TempData["ToastType"] = "danger";
                return View("Login");
            }
            HttpContext.Session.SetString("Username", user.Username);
            //System.Console.WriteLine("Login successful for user: " + user.Username);
            // Authentication successful, redirect to dashboard or home page
            TempData["ToastMessage"] = "Login successful. Welcome, " + user.FirstName + "!";
            TempData["ToastType"] = "success";
            return RedirectToAction("Dashboard","Inventory");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}