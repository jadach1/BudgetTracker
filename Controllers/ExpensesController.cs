using System.Globalization;
using System.Runtime.Serialization;
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
            //lets get the current month;
            int month = DateTime.Today.Month;
            int year = DateTime.Today.Year;
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            ViewData["month"]=dtfi.GetMonthName(month);
            ViewData["year"]=year;
            return View();
        }

      


    }
}