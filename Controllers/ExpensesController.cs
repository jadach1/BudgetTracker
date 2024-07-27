using System.Globalization;
using Budget_Man.Helper.Library;
using Budget_Man.Models;
using Budget_Man.Server.IUnitWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

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
        public IActionResult Index(IFormCollection form)
        {
            try
            {
                var given_month = form["month_number"];
                int month;
//:Microsoft.Extensions.Primitives.StringValues:

                if(!given_month.IsNullOrEmpty()){
                      month = int.Parse(given_month);
                } else {
                       month = DateTime.Today.Month;
                }
               
                int year = DateTime.Today.Year;
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();

                // integer of the month, for form-submission, in Script_ExpenseDBCalls
                ViewData["month_number"] = month;

                ViewData["month"] = dtfi.GetMonthName(month);
                ViewData["year"] = year;

                //Get Categories
                IEnumerable<Category> categories = _db.categoryRepository.GetAll();
                ViewData["categories"] = categories;

                MasterExpenseList masterExpenseList = new MasterExpenseList();

                //Get Expenses
                for (int i = 1; i < 6; i++)
                {
                    string str = "expenses" + i;
                    IEnumerable<Expenses> expenses = _db.expensesRepository.GetWeekOf(i, month);
                    masterExpenseList.appendExpenses(expenses, i, month, dtfi.GetMonthName(month));
                    ViewData[str] = expenses;
                }

                masterExpenseList.appendExpenseForm(categories, dtfi.GetMonthName(month), "Create");
                return View(masterExpenseList);
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IEnumerable<ExpensesFormPosting> expense)
        {
            try
            {
                int counter = 0;
                //Loop through the List of Expenses
                foreach (var item in expense)
                {
                    Expenses newExpense = new Expenses
                    {
                        Month = ExpensesFormPosting.GetMonthNumber_From_MonthName(item.Month),
                        Week = HelperFunctions.ConvertStringToInt(item.Week),
                        CategoryId = HelperFunctions.ConvertStringToInt(item.Type),
                        Amount = HelperFunctions.ConvertStringToFloat(item.Amount),
                        Date = item.Date,
                        Description = item.Description
                    };
                    counter++;
                    _db.expensesRepository.Add(newExpense);
                };

                _db.Save();

                //Update Expenses
                IEnumerable<Expenses> expenses = _db.expensesRepository.GetAll();
                ViewData["expenses"] = expenses;

                return this.Ok($"hello world" + counter);
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok($"hello world error");
            }
        }

    }
}