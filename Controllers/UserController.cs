using AssignmentJobPortal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AssignmentJobPortal.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AssignmentJobPortal.Controllers
{
    public class UserController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Register()
        {
            var roles = _dbContext.Roles.ToList();
            ViewBag.Roles = roles;

            Console.WriteLine($"Number of roles retrieved: {roles.Count}");
            foreach (var role in roles)
            {
                Console.WriteLine($"Role ID: {role.Id}, Name: {role.Name}");
            }

            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email or phone number already exists
                if (_dbContext.Users.Any(u => u.Email == model.Email || u.PhoneNumber == model.PhoneNumber))
                {
                    ModelState.AddModelError(string.Empty, "Email or Phone Number is already in use.");
                    ViewBag.Roles = await _dbContext.Roles.ToListAsync();
                    return View(model);
                }

                var user = new Users
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password, // Consider hashing the password before storing it
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DateOfBirth,
                    RoleId = model.RoleId
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Unable to create your user.";
            ViewBag.Roles = await _dbContext.Roles.ToListAsync();
            return View(model);
        }
        // GET: UserController/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users uvm)
        {
            try
            {
                // Fetch user by email
                var user = _dbContext.Users
                    .Where(u => u.Email == uvm.Email)
                    .FirstOrDefault();

                // Validate the user and password
                if (user != null && user.Password == uvm.Password)
                {
                    // Create claims including the role
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("RoleId", user.RoleId.ToString()) // Add role as claim
                    };

                    // Create identity using the claims
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Define authentication properties (optional)
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // Persistent cookie
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30) // Expiration time
                    };

                    // Sign in the user
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    // Redirect to the home page after successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // If login fails, show error message
                    ViewBag.Error = "Unable to login. Please check your credentials.";
                    return View();
                }
            }
            catch
            {
                // Return an internal server error status code in case of an exception
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the home page or login page after logout
            return RedirectToAction("Index", "Home");
        }


        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
                var user = _dbContext.Users.Find(id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Users user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
