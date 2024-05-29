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
        public async Task<IActionResult> Create([FromBody]Expenses2 expense)
        {
            Console.WriteLine(expense.Date);
            try
            {
                Expenses newExpense = new Expenses
                {
                    Month = Expenses2.GetMonthNumber_From_MonthName(expense.Month),
                    Week = HelperFunctions.ConvertStringToInt(expense.Week),
                    CategoryId = HelperFunctions.ConvertStringToInt(expense.Type),
                    Amount = HelperFunctions.ConvertStringToFloat(expense.Amount),
                    Date = expense.Date,
                    Description = expense.Description                
                };

                Console.WriteLine("is there hope " + expense.Description + " date: " + expense.Date + " Month: " + expense.Month);
                 _db.expensesRepository.Add(newExpense);
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