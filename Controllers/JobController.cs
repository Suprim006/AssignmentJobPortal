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
        public ActionResult Index()
        {
            var job_list = _dbContext.Jobs
                .Include(j => j.Company) // Eager load the Company
                .Include(j => j.Category) // Eager load the Category
                .Select(j => new JobViewModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    PostedDate = j.PostedDate,
                    ClosingDate = j.ClosingDate,
                    CompanyName = j.Company.Name, // Access the Company name
                    CategoryName = j.Category.Name // Access the Category name
                }).ToList();
            Console.WriteLine("Listing job_list");
            Console.WriteLine(job_list.Count);
            if (job_list.Count > 0)
            {
                Console.WriteLine(job_list.Count.ToString());
                return View(job_list);
            }

            return View(Enumerable.Empty<JobViewModel>());
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: JobController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobController/Edit/5
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

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobController/Delete/5
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
