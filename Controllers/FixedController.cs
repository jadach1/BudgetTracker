
using System.Runtime.CompilerServices;
using Budget_Man.Helper.Library;
using Budget_Man.Models;
using Budget_Man.Server.IUnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Budget_Man.Controllers;
 [Authorize]
public class FixedExpenses : Controller
{
    private IUnitOfWork _dbCentral;
    private readonly UserManager<IdentityUser> _userManager;
   private HelperFunctions _helperFunctions;
    public FixedExpenses(IUnitOfWork db,UserManager<IdentityUser> userManager,HelperFunctions helperFunctions)
    {
        _dbCentral = db;
        _userManager = userManager;
        _helperFunctions = helperFunctions;
    }

    public async Task<IActionResult> Index()
    {
         //GET USER 
        IdentityUser user = await GetActiveUser();
        IEnumerable<FixedExpense> objCategoryList = _dbCentral.fixedExpensesRepository.GetAll(user.Id);
        return View(objCategoryList);
    }

    //Fetch all fixed expenses, async ajax call
    public async Task<IActionResult> GetAll(){
         //GET USER 
        IdentityUser user = await GetActiveUser();
        IEnumerable<FixedExpense> fixedExpenses = _dbCentral.fixedExpensesRepository.GetAll(user.Id);
        return this.Ok(fixedExpenses);
    }

    [HttpPost]
    public async Task<IActionResult> Create(IFormCollection formCollection)
    {
        try
        {
            var name = formCollection["name"];
            var amount = formCollection["amount"];
            var currency = formCollection["currency"];
            //GET USER 
            var user = await _userManager.GetUserAsync(User);

            FixedExpense fe = new FixedExpense();
            fe.Name = formCollection["name"];
            fe.Amount = Single.Parse(formCollection["amount"]);
            fe.Currency = currency;
            fe.Userid = user.Id;

            _dbCentral.fixedExpensesRepository.Add(fe);
            _dbCentral.Save();
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("caught expection");
            Console.WriteLine(ex);
        }
        catch (FormatException fe)
        {
            Console.WriteLine(fe);
            Console.WriteLine("format exception");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine(" exception");
        }
        _helperFunctions.toasterTest("Created New Fixed-Expense",1);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(IFormCollection formCollection)
    {
        try
        {
             //GET USER 
            var user = await _userManager.GetUserAsync(User);

            FixedExpense fixedExpense = new FixedExpense();
            fixedExpense.Name = formCollection["name"];
            fixedExpense.Amount = Convert.ToSingle(formCollection["amount"]);
            fixedExpense.Id = Convert.ToInt32(formCollection["id"]);
            fixedExpense.Currency = formCollection["currency"];
            fixedExpense.Userid = user.Id;
            
            _dbCentral.fixedExpensesRepository.Update(fixedExpense);
            _dbCentral.Save();
             _helperFunctions.toasterTest("Fixed-Expense has been edited",3);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewData["errorMessage"] = "exception " + e;
            return View("Views/Errors/generalError.cshtml");
        }
    }
    [HttpPost]
    public IActionResult Delete(IFormCollection formCollection)
    {
        var id = Convert.ToInt32(formCollection["id"]);
        bool flag = _dbCentral.fixedExpensesRepository.Remove(id);
        // FixedExpense obj = _db.FixedExpense.Find(id);
        if (flag)
        {
            _dbCentral.Save();
             _helperFunctions.toasterTest("Fixed-Expense has been deleted",2);
            return RedirectToAction("Index");
        }
        else
        {
            ViewData["errorMessage"] = "Cannot find this ID " + id;
            return View("Views/Errors/generalError.cshtml");
        }
        
    }
        
    public async Task<IdentityUser> GetActiveUser()
        {
            try
            {
                //GET USER 
                var user = await _userManager.GetUserAsync(User);

                if (user == null) throw new Exception("Couldn't find user");

                return user;

            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "exception " + e;
                View("Views/Errors/generalError.cshtml");
                return null;
            }
        }
}