using System.Data;
using System.Diagnostics;
using BudgetAssistent.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAssistent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View("Chart");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public JsonResult NewChart()
        {
            List<object> iData = new List<object>();
            // Creating sample data
            DataTable dt = new DataTable();
            dt.Columns.Add("Employee", System.Type.GetType("System.String"));
            dt.Columns.Add("Credit", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();
            dr["Employee"] = "Sam";
            dr["Credit"] = 123;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Employee"] = "Alex";
            dr["Credit"] = 456;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Employee"] = "Michael";
            dr["Credit"] = 587;
            dt.Rows.Add(dr);
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
