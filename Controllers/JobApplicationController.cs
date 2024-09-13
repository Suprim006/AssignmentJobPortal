using AssignmentJobPortal.Entities;
using AssignmentJobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AssignmentJobPortal.Controllers
{
    [Authorize]
    public class JobApplicationController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();
        // GET: JobApplication
        public ActionResult Index()
        {
            var jobApplications = _dbContext.JobApplications
                .Include(ja => ja.User)
                .Include(ja => ja.Job)
                .Select(ja => new JobApplicationViewModel()
                {
                    Id = ja.Id,
                    ApplicationDate = ja.ApplicationDate,
                    UserName = ja.User.FirstName + " " + ja.User.LastName,
                    JobTitle = ja.Job.Title,
                    CoverLetter = ja.CoverLetter
                })
                .ToList();

            return View(jobApplications);
        }

        // GET: JobApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobApplication/Create
        public IActionResult Create(int jobId, int userId)
        {
            // Assuming you have DbContext set up to access your Job and User tables
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var job = _dbContext.Jobs.FirstOrDefault(j => j.Id == jobId);

            if (user == null || job == null)
            {
                return NotFound();
            }

            var model = new JobApplicationViewModel
            {
                JobId = jobId,
                UserId = userId,
                ApplicationDate = DateTime.Now,
                UserName = $"{user.FirstName} {user.LastName}", // New property for full name
                JobTitle = job.Title // New property for job title
            };

            return View(model);
        }




        // POST: JobApplication/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobApplicationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var jobApplication = new JobApplications
                {
                    ApplicationDate = viewModel.ApplicationDate,
                    UserId = viewModel.UserId,
                    JobId = viewModel.JobId,
                    CoverLetter = viewModel.CoverLetter
                };

                _dbContext.JobApplications.Add(jobApplication);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Unable to create new company";
            return RedirectToAction("Index");
        }


        // GET: JobApplication/Edit/5
        public ActionResult Edit(int id)
        {
            var jobApplication = _dbContext.JobApplications
                .Include(ja => ja.User)
                .Include(ja => ja.Job)
                .Where(ja => ja.Id == id)
                .Select(ja => new JobApplicationViewModel
                {
                    Id = ja.Id,
                    ApplicationDate = ja.ApplicationDate,
                    UserId = ja.UserId,
                    JobId = ja.JobId,
                    CoverLetter = ja.CoverLetter
                })
                .FirstOrDefault();

            if (jobApplication == null)
            {
                return NotFound();
            }

            // Re-populate drop-down lists
            ViewBag.Jobs = _dbContext.Jobs.Select(j => new SelectListItem
            {
                Value = j.Id.ToString(),
                Text = j.Title
            }).ToList();

            ViewBag.Users = _dbContext.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName
            }).ToList();

            return View(jobApplication);
        }


        // POST: JobApplication/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, JobApplicationViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var jobApplication = _dbContext.JobApplications.Find(id);
                if (jobApplication != null)
                {
                    jobApplication.ApplicationDate = viewModel.ApplicationDate;
                    jobApplication.UserId = viewModel.UserId;
                    jobApplication.JobId = viewModel.JobId;
                    jobApplication.CoverLetter = viewModel.CoverLetter;

                    _dbContext.JobApplications.Update(jobApplication);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            // Re-populate drop-down lists
            ViewBag.Jobs = _dbContext.Jobs.Select(j => new SelectListItem
            {
                Value = j.Id.ToString(),
                Text = j.Title
            }).ToList();

            ViewBag.Users = _dbContext.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName
            }).ToList();

            return View(viewModel);
        }


        // GET: JobApplication/Delete/5
        public ActionResult Delete(int id)
        {
            var jobApplication = _dbContext.JobApplications
                .Include(ja => ja.User)
                .Include(ja => ja.Job)
                .Where(ja => ja.Id == id)
                .Select(ja => new JobApplicationViewModel
                {
                    Id = ja.Id,
                    ApplicationDate = ja.ApplicationDate,
                    UserName = ja.User.FirstName + " " + ja.User.LastName,
                    JobTitle = ja.Job.Title,
                    CoverLetter = ja.CoverLetter
                })
                .FirstOrDefault();

            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }


        // POST: JobApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var jobApplication = _dbContext.JobApplications.Find(id);
            if (jobApplication != null)
            {
                _dbContext.JobApplications.Remove(jobApplication);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
