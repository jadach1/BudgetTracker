using System.Globalization;
using System.Runtime.Serialization;
using Budget_Man.Helper.Library;
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
            ViewData["month"] = dtfi.GetMonthName(month);
            ViewData["year"] = year;

            //Get Categories
            IEnumerable<Category> categories = _db.categoryRepository.GetAll();
            ViewData["categories"] = categories;

            //create model to pass to view
            createExpenseForm model = new createExpenseForm(categories, dtfi.GetMonthName(month), "Create");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IEnumerable<Expenses2> expense)
        {
            try
            {
                //Loop through the List of Expenses
                foreach (var item in expense)
                {
                    Expenses newExpense = new Expenses
                    {
                        Month = Expenses2.GetMonthNumber_From_MonthName(item.Month),
                        Week = HelperFunctions.ConvertStringToInt(item.Week),
                        CategoryId = HelperFunctions.ConvertStringToInt(item.Type),
                        Amount = HelperFunctions.ConvertStringToFloat(item.Amount),
                        Date = item.Date,
                        Description = item.Description
                    };
                    Console.WriteLine("is there hope " + item.Description + " date: " + item.Date + " Month: " + item.Month);
                    _db.expensesRepository.Add(newExpense);
                };

                _db.Save();
                return this.Ok($"hello world");
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok($"hello world error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSave()
        {
            try
            {
                Console.WriteLine("finito");

                return this.Ok($"Saved good maybe");
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok($"bot good error");
            }
        }





    }
}