
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
}