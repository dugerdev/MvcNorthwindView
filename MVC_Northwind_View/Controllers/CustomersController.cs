using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Northwind_View.Models;

namespace MVC_Northwind_View.Controllers
{
    public class CustomersController : Controller
    {
        private readonly NorthwindContext _context;

        public CustomersController(NorthwindContext context)
        {
            _context = context;
        }

        // Index
        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        // Create - GET
        public IActionResult Create()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Update - GET
        public IActionResult Update(string id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // Update - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Customer model)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(string id)
        {
            var customer = _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails) 
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer != null)
            {
            
                foreach (var order in customer.Orders)
                {
                    if (order.OrderDetails.Any())
                    {
                        _context.OrderDetails.RemoveRange(order.OrderDetails);
                    }
                }

               
                if (customer.Orders.Any())
                {
                    _context.Orders.RemoveRange(customer.Orders);
                }

                
                _context.Customers.Remove(customer);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


    }
}
