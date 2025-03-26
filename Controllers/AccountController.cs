using Microsoft.AspNetCore.Mvc;
using RoleBasedAuth.Repository;

namespace RoleBasedAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.ValidateUser(username, password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Store User Information in Session
            HttpContext.Session.SetString("Username", user["Username"].ToString());
            HttpContext.Session.SetString("Role", user["RoleId"].ToString());

            return RedirectToAction("Index", "Home");
        }

        // Logout: Clear Session

        [AuthorizeRole("1", "2")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
