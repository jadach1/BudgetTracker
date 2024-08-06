using System.Globalization;
using Budget_Man.Helper.Library;
using Budget_Man.Models;
using Budget_Man.Server.IUnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Budget_Man.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        // private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _db;
        private readonly UserManager<IdentityUser> _userManager;
        public ExpensesController(IUnitOfWork db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync(IFormCollection form)
        {
            try
            {
                var given_month = form["month_number"];
                int month;

                if (!given_month.IsNullOrEmpty())
                {
                    month = int.Parse(given_month);
                }
                else
                {
                    month = DateTime.Today.Month;
                }

                int year = DateTime.Today.Year;
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();

                // integer of the month, for form-submission, in Script_ExpenseDBCalls
                ViewData["month_number"] = month;

                ViewData["month"] = dtfi.GetMonthName(month);
                ViewData["year"] = year;
                
                //FETCH, FROM DB, CATEGORIES
                IEnumerable<Category> categories = _db.categoryRepository.GetAll();
                ViewData["categories"] = categories;

                // PREPARE 
                MasterExpenseList masterExpenseList = new MasterExpenseList();

                //GET USER  
                var user = await _userManager.GetUserAsync(User);
                
                //FETCH, FROM DB, Get Expenses
                for (int i = 1; i < 6; i++)
                {
                    string str = "expenses" + i;
                    IEnumerable<Expenses> expenses = _db.expensesRepository.GetWeekOf(i, month,user.Id);
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
                //GET USER 
                var user = await _userManager.GetUserAsync(User);
                int counter = 0;
                //Loop through the List of Expenses
                foreach (var item in expense)
                {
                    Console.WriteLine("come " + item.Currency);
                    Console.WriteLine(item);
                    Expenses newExpense = new Expenses
                    {
                        Month = ExpensesFormPosting.GetMonthNumber_From_MonthName(item.Month),
                        Week = HelperFunctions.ConvertStringToInt(item.Week),
                        CategoryId = HelperFunctions.ConvertStringToInt(item.Type),
                        Amount = HelperFunctions.ConvertStringToFloat(item.Amount),
                        Date = item.Date,
                        Description = item.Description,
                        MyUserName = user.Id,
                        Currency = item.Currency
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