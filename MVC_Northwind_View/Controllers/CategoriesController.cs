using Microsoft.AspNetCore.Mvc;
using MVC_Northwind_View.Models;

namespace MVC_Northwind_View.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            List<Category> categories = [.. _context.Categories];
            return View(categories);
        }

        // GET: Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Categories/Update
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null)
                return NotFound();

            Category? category = _context.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int? id, Category model)
        {
            if (id is null) return NotFound();

            if(id != model.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Categories.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();

            Category category = _context.Categories.Find(id);

            if(category == null) return NotFound();

            _context.Categories.Remove(category);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
            
        }
    }
}
