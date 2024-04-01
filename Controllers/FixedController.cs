
using Budget_Man.Models;
using Budget_Man.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Controllers;
public class FixedExpenses : Controller {
    private ApplicationDbContext _db;

    public FixedExpenses(ApplicationDbContext db){
        _db = db;
    }

    public IActionResult Index(){
        List<FixedExpense> objCategoryList= _db.FixedExpense.ToList();
        return View(objCategoryList);
    }

    [HttpPost]
    public IActionResult Create(IFormCollection formCollection){
        Console.WriteLine("here we are");
        Console.WriteLine("here we are, " + formCollection["name"]);
        Console.WriteLine("here we are " + formCollection["amount"]);
        if(ModelState.IsValid){
            FixedExpense fe = new FixedExpense(_db);
            fe.Name = formCollection["name"];
            fe.Amount = Single.Parse(formCollection["amount"]);
            fe.save(fe);
            return RedirectToAction("Index");
        } else {
            return RedirectToAction("Index");
        }
      
    }
}