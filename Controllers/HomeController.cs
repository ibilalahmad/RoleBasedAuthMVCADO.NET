using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Models;
using RoleBasedAuth.Repository;

namespace RoleBasedAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if user is logged in
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }

        [AuthorizeRole("1")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        // Alternate Way without using AuthorizeRoleAttribute custom class

        //public IActionResult AdminDashboard()
        //{
        //    if (HttpContext.Session.GetString("Role") != "1")
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    return View();
        //}

        [AuthorizeRole("2")]
        public IActionResult UserDashboard()
        {
            return View();
        }

        [AuthorizeRole("1", "2")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
