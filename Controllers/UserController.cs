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
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User uvm)
        {
            try
            {
                ViewBag.Error = string.Empty;
                if (_dbContext.Users.Where(u => u.Email == uvm.Email).Any())
                {
                    ViewBag.Error = "User with this email already exists";
                    return View();
                }
                var _usr = new User()
                {
                    FirstName = uvm.FirstName,
                    LastName = uvm.LastName,
                    Email = uvm.Email,
                    PhoneNumber = uvm.PhoneNumber,
                    Password = uvm.Password,
                    DateOfBirth = uvm.DateOfBirth
                };

                _dbContext.Users.Add(_usr);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }
        // GET: UserController/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User uvm)
        {
            try
            {
                var user = _dbContext.Users
                    .Where(u => u.Email == uvm.Email)
                    .FirstOrDefault();

                if (user != null && user.Password == uvm.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                    };
                    var claimsIdntity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties();

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdntity),
                            authProperties
                        );
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Unable to login";
                    return View();
                }
            }
            catch
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }


        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
