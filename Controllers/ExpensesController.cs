using Budget_Man.Server;
using Microsoft.AspNetCore.Mvc;

namespace Budget_Man.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExpensesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public void OnPost(string name, string email)
        {
            ViewData["confirmation"] = $"{name}, information will be sent to {email}";
        }


    }
}