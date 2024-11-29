using Microsoft.AspNetCore.Mvc;
using BudgetAssistent.Data;
using BudgetAssistent.Models;

namespace BudgetAssistent.Controllers
{
    public class AssistentController : Controller
    {
        private ApplicationDbContext db;

        public AssistentController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Expenses data)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Browse()
        {
            var data = db.Expenses.ToList();
            return View(data);
        }
    }
}
