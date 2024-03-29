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
            List<Category> objCategoryList= _db.Categories.ToList();
            return View(objCategoryList);
        }
         public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj){
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Category obj){
            _db.Categories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id){
            if(id != null && id != 0 ){
                Category obj = _db.Categories.Find(id);
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            } else {
                return Content("Id " + id+ "not found");
            }
         
        }
    }
}
