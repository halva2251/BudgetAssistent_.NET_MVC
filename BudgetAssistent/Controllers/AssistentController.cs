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
            var expenses = transactions.Where(m => m.Type == ExpensesType.Expense).ToList();
            var incomes = transactions.Where(m => m.Type == ExpensesType.Income).ToList();
            List<object> iData = new List<object>();

            // Creating sample data
            DataTable dt = new DataTable();
            
            dt.Columns.Add("Date", System.Type.GetType("System.String"));
            dt.Columns.Add("Expenses", System.Type.GetType("System.Double"));
            dt.Columns.Add("Income", System.Type.GetType("System.Double"));
            expenses = expenses.OrderBy(m => m.Date).ToList();
            foreach (var expence in expenses)
            {
                DataRow dr = dt.NewRow();
                dr["Date"] = expence.Date;
                dr["Expenses"] = expence.Amount;
                dt.Rows.Add(dr);
            }
            incomes = incomes.OrderBy(m => m.Date).ToList();
            foreach (var income in incomes)
            {
                DataRow dr = dt.NewRow();
                dr["Date"] = income.Date;
                dr["Income"] = income.Amount;
                dt.Rows.Add(dr);
            }

            //DataRow dr = dt.NewRow();
            //dr["Date"] = "Sam";
            //dr["Amount"] = 123;
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["Date"] = "Alex";
            //dr["Amount"] = 456;
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["Date"] = "Michael";
            //dr["Amount"] = 587;
            //dt.Rows.Add(dr);

            // Looping and extracting each DataColumn to List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            // Source data returned as JSON
            return Json(iData);
        }
    }
}
