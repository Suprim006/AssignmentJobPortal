using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentJobPortal.Controllers
{
    public class JobApplicationController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();
        // GET: JobApplicationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: JobApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobApplicationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobApplicationController/Create
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

        // GET: JobApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobApplicationController/Edit/5
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

        // GET: JobApplicationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobApplicationController/Delete/5
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
