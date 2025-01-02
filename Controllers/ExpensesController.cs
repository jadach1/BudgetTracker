using System.Globalization;
using System.Linq.Expressions;
using Budget_Man.AuthService.Models;
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
        private readonly UserManager<MyUser> _userManager;

        private HelperFunctions _helperFunctions;

        public ExpensesController(IUnitOfWork db, UserManager<MyUser> userManager, HelperFunctions helperFunctions)
        {
            _db = db;
            _userManager = userManager;
            _helperFunctions = helperFunctions;
        }
        public async Task<IActionResult> IndexAsync(IFormCollection form)
        {
            try
            {
                 //GET USER  
                var user = await _userManager.GetUserAsync(User);

                var given_month = form["month_number"];
                int month;

                if (!given_month.IsNullOrEmpty())
                    month = int.Parse(given_month);
                 else
                    month = DateTime.Today.Month;
                
              
                int year = (int)user.year;
                
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();

                // integer of the month, for form-submission, in Script_ExpenseDBCalls
                ViewData["month_number"] = month;
                ViewData["month"] = dtfi.GetMonthName(month);
                ViewData["year"] = year;

               

                //FETCH, FROM DB, CATEGORIES
                IEnumerable<Category> categories = _db.categoryRepository.GetAll(user.Id);
                ViewData["categories"] = categories;

                // PREPARE 
                MasterExpenseList masterExpenseList = new MasterExpenseList();

                //FETCH, FROM DB, Transaction
                for (int i = 1; i < 6; i++)
                {
                    // string str = "expenses" + i;
                    IEnumerable<Expenses> expenses = _db.expensesRepository.GetWeekOf(i, month,year, user.Id);
                    masterExpenseList.appendExpenses(expenses, i, month, dtfi.GetMonthName(month));
                    // ViewData[str] = expenses;
                }

                //Fetch Fixed expenses, if they exist.
                IEnumerable<Expenses> fixedexpense = _db.expensesRepository.GetFixedExpenses(month,year, user.Id);
                masterExpenseList.fixedExpenses = new DisplayExpenses( fixedexpense , 0, month, dtfi.GetMonthName(month),false);
                //If the user has imported fixed expenses for this month, then set the flag to true, so it will appear in the expenses table
                if(masterExpenseList.fixedExpenses != null){
                    ViewData["FixedExpenses"] = "true";
                }

                 //Fetch Income Transaction, if they exist.
                IEnumerable<Expenses> income = _db.expensesRepository.GetIncomeExpenses(month,year, user.Id);
                masterExpenseList.incomeExpenses = new DisplayExpenses( income , 0, month, dtfi.GetMonthName(month),false);
                //If the user has imported fixed expenses for this month, then set the flag to true, so it will appear in the expenses table
                if(masterExpenseList.incomeExpenses != null){
                    ViewData["IncomeExpenses"] = "true";
                }
                
                //Creates the expense form, via model class MasterExpenseList mehtod
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
                        Year = HelperFunctions.ConvertStringToInt(item.Year),
                        Month = ExpensesFormPosting.GetMonthNumber_From_MonthName(item.Month),
                        Week = HelperFunctions.ConvertStringToInt(item.Week),
                        CategoryId = HelperFunctions.ConvertStringToInt(item.Type),
                        Amount = HelperFunctions.ConvertStringToFloat(item.Amount),
                        Date = item.Date,
                        Description = item.Description,
                        MyUserName = user.Id,
                        Currency = item.Currency,
                       isIncome = item.isIncome
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

        //When user wants to import fixed expenses, this will take a list of 
        //expenses which are based off of the users fixed expenses for the month
        [HttpPost]
        public async Task<IActionResult> CreateFixedExpenses([FromBody] IEnumerable<Expenses> expense)
        {
            try
            {
                //GET USER 
                var user = await _userManager.GetUserAsync(User);
                int counter = 0;
                
                //Loop through the List of Expenses
                foreach (var item in expense)
                {
                    counter++;
                    _db.expensesRepository.Add(item);
                };

                _db.Save();
                //Pop up confirmation success
                return this.Ok($"Successfully added " + counter + " fixed expenses");
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return this.Ok(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne(Expenses expense)
        {
            try
            {
                //GET USER 
                var user = await _userManager.GetUserAsync(User);
                expense.MyUserName = user.Id;
                _db.expensesRepository.Add(expense);
                _db.Save();

                return Redirect("Index");
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "exception creating a new single expense " + e;
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
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = _db.expensesRepository.Remove(id);
                if (result)
                {
                    _db.Save();
                    return this.Ok("successfully deleted expense !" + id);
                }
                else
                {
                    return this.Ok("Could not delete expense ");
                }

            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "error when deleting expense item " + e;
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
                Expenses expense = await _db.expensesRepository.GetSingleExpense(id, user.Id);
                Expression<Func<Category, bool>> filter = o => o.Id == expense.CategoryId;
                Category category = await _db.categoryRepository.Get(filter);
                expense.category = category;
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
        
        public async Task<bool> DoesExpenseExist(int id){
            try
            {
                //GET USER 
                var user = await _userManager.GetUserAsync(User);
                bool result = await _db.expensesRepository.GetExpensesWithCategoryId(id, user.Id);
                if(result)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "Problem here.  Getting an expense based on a category seems to cause this issue: " + e;
                View("Views/Errors/generalError.cshtml");
                return false;
            }
        }
      [HttpPost]
       public async Task<bool> ChangeExpenseCategory(int CategoryId_Old, int CategoryId_New){
            try
            {
                //GET USER 
                var user = await _userManager.GetUserAsync(User);
                bool result = await _db.expensesRepository.Update_Change_Expense_Category(user.Id, CategoryId_Old, CategoryId_New);
                _db.Save();
                if(result)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "Error, changing expense of one category to another" + e;
                View("Views/Errors/generalError.cshtml");
                return false;
            }
       }
         
    }
}