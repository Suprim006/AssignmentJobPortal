using AssignmentJobPortal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentJobPortal.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();
        // GET: Category
        public ActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Create(Categories category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }


        // GET: Category/Edit/5
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Edit(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Edit(int id, Categories category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }


        // GET: Category/Delete/5
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManagerRole")]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
