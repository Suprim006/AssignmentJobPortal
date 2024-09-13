using AssignmentJobPortal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentJobPortal.Controllers
{
    public class CompanyController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();
        // GET: Company
        public ActionResult Index()
        {
            var companies = _dbContext.Companies
                .Include(c => c.Jobs) // Eagerly load the related Jobs
                .ToList();
            return View(companies);
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyController/Create
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Create(Companies company)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Companies.Add(company);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Unable to create new company";
            return View(company);
        }


        // GET: Company/Edit/5
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Edit(int id)
        {
            var company = _dbContext.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Edit(int id, Companies company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Companies.Update(company);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }


        // GET: Company/Delete/5
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Delete(int id)
        {
            var company = _dbContext.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult DeleteConfirmed(int id)
        {
            var company = _dbContext.Companies.Find(id);
            if (company != null)
            {
                _dbContext.Companies.Remove(company);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
