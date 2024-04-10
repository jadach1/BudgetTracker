
using Budget_Man.Models;
using Budget_Man.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Controllers;
public class FixedExpenses : Controller
{
    private ApplicationDbContext _db;

    public FixedExpenses(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        List<FixedExpense> objCategoryList = _db.FixedExpense.ToList();
        return View(objCategoryList);
    }

    [HttpPost]
    public IActionResult Create(IFormCollection formCollection)
    {
        try
        {
            var name = formCollection["name"];
            var amount = formCollection["amount"];

            FixedExpense fe = new FixedExpense(_db);
            fe.Name = formCollection["name"];
            fe.Amount = Single.Parse(formCollection["amount"]);
            fe.save(fe);
        }
        catch (ArgumentNullException ex) {
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

        Console.WriteLine(" go");
        return RedirectToAction("Index");
    }



    [HttpPost]
    public IActionResult Delete(IFormCollection formCollection)
    {
        var id = Convert.ToInt32(formCollection["id"]);
        FixedExpense obj = _db.FixedExpense.Find(id);
        if (obj != null)
        {
            _db.FixedExpense.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            ViewData["errorMessage"] = "Cannot find this ID " + id;
            return View("Views/Errors/generalError.cshtml");
        }

    }

}