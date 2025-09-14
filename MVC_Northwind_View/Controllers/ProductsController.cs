using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Northwind_View.Models;

namespace MVC_Northwind_View.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _context;

        public ProductsController(NorthwindContext context)
        {
            _context = context;
        }

        // INDEX - Listeleme
        public IActionResult Index()
        {
            List<Product> products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToList();

            return View(products);
        }

        // CREATE - GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", model.SupplierId);

            return View(model);
        }

        // UPDATE - GET
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();

            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", product.SupplierId);

            return View(product);
        }

        // UPDATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int? id, Product model)
        {
            if (id is null) return NotFound();
            if (id != model.ProductId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Products.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", model.SupplierId);

            return View(model);
        }

        // DELETE
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();

            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
