using Budget_Man.Server;
using Budget_Man.Models;
using Microsoft.AspNetCore.Mvc;

namespace Budget_Man.Controllers{
    public class CategoryController : Controller {
        private readonly ApplicationDbContext _db;
        public  CategoryController(ApplicationDbContext db)
        {
            _db=db;
        }
        public IActionResult Index(){
            
            return View();
        }
         public IActionResult Create(){
            
            return View();
        }
        [HttpPost]
        public void Create(Category obj){
            _db.Categories.Add(obj);
            _db.SaveChanges();
            Console.WriteLine("Created object");
        }
    }
}
