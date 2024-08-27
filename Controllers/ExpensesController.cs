using System.Globalization;
using System.Linq.Expressions;
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

        private HelperFunctions _helperFunctions;

        public ExpensesController(IUnitOfWork db, UserManager<IdentityUser> userManager, HelperFunctions helperFunctions)
        {
            _db = db;
            _userManager = userManager;
            _helperFunctions = helperFunctions;
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
                    IEnumerable<Expenses> expenses = _db.expensesRepository.GetWeekOf(i, month, user.Id);
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
                return this.Ok(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Expenses obj)
        {
            try
            {
                //GET USER 
                var user = await _userManager.GetUserAsync(User);
                obj.MyUserName = user.Id;
                _db.expensesRepository.Update(obj);
                _db.Save();
                _helperFunctions.toasterTest("Successfully edited expense", 1);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _helperFunctions.toasterTest("Failed at editing expense", 2);
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok(e);
            }
        }

        public async Task<IActionResult> GetOneExpense(int id)
        {
            try
            {
                //GET USER 
                var user = await _userManager.GetUserAsync(User);
                Expenses expense = await _db.expensesRepository.GetSingleExpense(id,user.Id);
                Expression<Func<Category, bool>> filter = o=> o.Id == expense.CategoryId;
                Category category = await _db.categoryRepository.Get(filter);
                expense.category = category;
                _helperFunctions.toasterTest("Successfully edited expense", 1);
                return this.Ok(expense);
            }
            catch (Exception e)
            {
                _helperFunctions.toasterTest("Failed at getting expense", 2);
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok(e);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _db.expensesRepository.Remove(id);
                return this.Ok("successfully deleted expense !" + id);
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "error when deleting expense item " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok(e);
            }

        }
    }
}