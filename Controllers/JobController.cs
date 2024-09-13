using AssignmentJobPortal.Entities;
using AssignmentJobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentJobPortal.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();

        // GET: JobController
        public ActionResult Index(int? categoryId, string location, int? postedWithin, string sort)
        {
            var query = _dbContext.Jobs
                .Include(j => j.Company)
                .Include(j => j.Category)
                .AsQueryable();

            // Apply filters
            if (categoryId.HasValue)
            {
                query = query.Where(j => j.Category.Id == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(j => j.Company.Address.Contains(location));
            }

            if (postedWithin.HasValue)
            {
                var cutoffDate = DateTime.Now.AddDays(-postedWithin.Value);
                query = query.Where(j => j.PostedDate >= cutoffDate);
            }

            // Apply sorting
            switch (sort)
            {
                case "date":
                    query = query.OrderByDescending(j => j.PostedDate);
                    break;
                case "salary":
                    query = query.OrderByDescending(j => j.Salary);
                    break;
                default:
                    query = query.OrderByDescending(j => j.PostedDate);
                    break;
            }

            var job_list = query.Select(j => new JobViewModel()
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                Salary = j.Salary,
                PostedDate = j.PostedDate,
                ClosingDate = j.ClosingDate,
                CompanyName = j.Company.Name,
                CompanyAddress = j.Company.Address,
                CategoryName = j.Category.Name,
                CategoryId = j.Category.Id
            }).ToList();

            // Populate ViewBag with data for dropdowns
            ViewBag.Categories = _dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();

            ViewBag.Locations = _dbContext.Companies
                .Select(c => c.Address)
                .Distinct()
                .ToList();

            ViewBag.Companies = _dbContext.Companies
                .Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Website = c.Website,
                    ContactEmail = c.ContactEmail,
                    ContactPhone = c.ContactPhone,
                    JobCount = c.Jobs.Count()
                })
                .ToList();

            return View(job_list);
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            var job_detail = _dbContext.Jobs
                .Include(j => j.Company)
                .Include(j => j.Category)
                .Where(j => j.Id == id)
                .Select(j => new JobViewModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    Salary = j.Salary,
                    PostedDate = j.PostedDate,
                    ClosingDate = j.ClosingDate,
                    CompanyName = j.Company.Name,
                    CompanyAddress = j.Company.Address,
                    CategoryName = j.Category.Name,
                })
                .FirstOrDefault();

            if (job_detail == null)
            {
                return NotFound();
            }

            return View(job_detail);
        }

        // GET: JobController/Create
        public ActionResult Create()
        {
            ViewBag.Companies = _dbContext.Companies.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();

            return View();
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jobs model)
        {
            if (ModelState.IsValid)
            {
                var job = new Jobs
                {
                    Title = model.Title,
                    Description = model.Description,
                    Salary = model.Salary,
                    PostedDate = model.PostedDate,
                    ClosingDate = model.ClosingDate,
                    CompanyId = model.CompanyId,
                    CategoryId = model.CategoryId
                };

                _dbContext.Jobs.Add(job);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            // If we got here, something went wrong; redisplay the form
            ViewBag.Companies = _dbContext.Companies.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View(model);
        }


        // GET: JobController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var job = _dbContext.Jobs
                .Include(j => j.Company) // Eager load the Company
                .Include(j => j.Category) // Eager load the Category
                .Where(j => j.Id == id)
                .Select(j => new Jobs
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    Salary = j.Salary,
                    PostedDate = j.PostedDate,
                    ClosingDate = j.ClosingDate,
                    CompanyId = j.CompanyId, // Optional: If you need it in the view
                    CategoryId= j.CategoryId // Optional: If you need it in the view
                })
                .FirstOrDefault();

            if (job == null)
            {
                return NotFound();
            }

            // Prepare data for dropdowns
            ViewBag.Companies = _dbContext.Companies.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();

            return View(job);
        }


        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jobs model)
        {
            if (ModelState.IsValid)
            {
                var job = _dbContext.Jobs.Find(model.Id);

                if (job != null)
                {
                    job.Title = model.Title;
                    job.Description = model.Description;
                    job.Salary = model.Salary;
                    job.PostedDate = model.PostedDate;
                    job.ClosingDate = model.ClosingDate;
                    job.CompanyId = model.CompanyId;
                    job.CategoryId = model.CategoryId;

                    _dbContext.Jobs.Update(job);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

            // If we got here, something went wrong; redisplay the form
            ViewBag.Companies = _dbContext.Companies.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();
            return View(model);
        }


        // GET: JobController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var job = _dbContext.Jobs
                .Include(j => j.Company) // Eager load the Company
                .Include(j => j.Category) // Eager load the Category
                .Where(j => j.Id == id)
                .Select(j => new JobViewModel
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    Salary = j.Salary,
                    PostedDate = j.PostedDate,
                    ClosingDate = j.ClosingDate,
                    CompanyName = j.Company.Name, // Optional: If you need it in the view
                    CategoryName = j.Category.Name // Optional: If you need it in the view
                })
                .FirstOrDefault();

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: JobController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var job = _dbContext.Jobs.Find(id);

            if (job != null)
            {
                _dbContext.Jobs.Remove(job);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
