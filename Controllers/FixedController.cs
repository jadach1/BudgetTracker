
using Budget_Man.Models;
using Budget_Man.Server.IUnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget_Man.Controllers;
 [Authorize]
public class FixedExpenses : Controller
{
    private IUnitOfWork _dbCentral;

    public FixedExpenses(IUnitOfWork db)
    {
        _dbCentral = db;
    }

    public IActionResult Index()
    {
        IEnumerable<FixedExpense> objCategoryList = _dbCentral.fixedExpensesRepository.GetAll();
        return View(objCategoryList);
    }

    [HttpPost]
    public IActionResult Create(IFormCollection formCollection)
    {
        try
        {
            var name = formCollection["name"];
            var amount = formCollection["amount"];

            FixedExpense fe = new FixedExpense();
            fe.Name = formCollection["name"];
            fe.Amount = Single.Parse(formCollection["amount"]);
         
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

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Edit(IFormCollection formCollection)
    {
        try
        {
            FixedExpense fixedExpense = new FixedExpense();
            fixedExpense.Name = formCollection["name"];
            fixedExpense.Amount = Convert.ToSingle(formCollection["amount"]);
            fixedExpense.Id = Convert.ToInt32(formCollection["id"]);
            _dbCentral.fixedExpensesRepository.Update(fixedExpense);
            _dbCentral.Save();
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
            return RedirectToAction("Index");
        }
        else
        {
            ViewData["errorMessage"] = "Cannot find this ID " + id;
            return View("Views/Errors/generalError.cshtml");
        }
    }
}