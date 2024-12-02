using Microsoft.AspNetCore.Mvc;
using BudgetAssistent.Data;
using BudgetAssistent.Models;
using System.Data;

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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = db.Expenses.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Expenses data)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Update(data);
                db.SaveChanges();
                return RedirectToAction("Browse");
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = db.Expenses.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Expenses data)
        {
            db.Expenses.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Browse");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = db.Expenses.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Details(Expenses data)
        {
            return RedirectToAction("Browse");
        }

        [HttpGet]
        public IActionResult SavingsCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SavingsCreate(SavingsGoal data)
        {
            if (ModelState.IsValid)
            {
                db.SavingsGoal.Add(data);
                db.SaveChanges();
                return RedirectToAction("SavingsCreate");
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Overview()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetBudget()
        {
            var transactions = db.Expenses.ToList();
            var expenses = transactions.Where(m => m.Type == ExpensesType.Expense)
                                      .OrderBy(m => m.Date)
                                      .ToList();
            var incomes = transactions.Where(m => m.Type == ExpensesType.Income)
                                     .OrderBy(m => m.Date)
                                     .ToList();

            // Get all unique dates
            var allDates = transactions.Select(t => t.Date)
                                      .Distinct()
                                      .OrderBy(d => d)
                                      .ToList();

            // Create lists for chart data
            var dates = new List<string>();
            var expenseAmounts = new List<double>();
            var incomeAmounts = new List<double>();

            // For each date, get the corresponding expense and income
            foreach (var date in allDates)
            {
                dates.Add(date.ToString("MM/dd/yyyy")); // Or whatever date format you prefer

                var expenseAmount = expenses.FirstOrDefault(e => e.Date == date)?.Amount ?? 0;
                var incomeAmount = incomes.FirstOrDefault(i => i.Date == date)?.Amount ?? 0;

                expenseAmounts.Add(expenseAmount);
                incomeAmounts.Add(incomeAmount);
            }

            var chartData = new List<object>
    {
        dates,              // Labels (dates) - will be aData[0]
        expenseAmounts,     // First dataset (expenses) - will be aData[1]
        incomeAmounts      // Second dataset (incomes) - will be aData[2]
    };

            return Json(chartData);
        }
    }
}
