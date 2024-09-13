using AssignmentJobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentJobPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        AppDbContext _dbContext = new AppDbContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            var companies = _dbContext.Companies.ToList();
            ViewBag.Categories = categories;
            ViewBag.Companies = companies;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AccessDenied()
        {
            // Pass an error message to the view or use TempData to show a message
            TempData["ErrorMessage"] = "You are not authorized to access this page.";
            return RedirectToAction("Index");
        }
    }
}
