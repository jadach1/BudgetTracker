using System.Globalization;
using System.Runtime.Serialization;
using Budget_Man.Models;
using Budget_Man.Server;
using Budget_Man.Server.IUnitWork;
using Microsoft.AspNetCore.Mvc;

namespace Budget_Man.Controllers
{
    public class ExpensesController : Controller
    {
       // private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _db;
        public ExpensesController(IUnitOfWork db)
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

            //Get Categories
            IEnumerable<Category> categories = _db.categoryRepository.GetAll();
            ViewData["categories"]=categories;

            //create model to pass to view
            createExpenseForm model = new createExpenseForm(categories, dtfi.GetMonthName(month), "Create");
            return View(model);
        }

      


    }
}