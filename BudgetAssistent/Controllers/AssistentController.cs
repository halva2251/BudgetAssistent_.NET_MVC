using Microsoft.AspNetCore.Mvc;

namespace BudgetAssistent.Controllers
{
    public class AssistentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
