using Budget_Man.Server;
using Budget_Man.Models;
using Microsoft.AspNetCore.Mvc;
using Budget_Man.Server.IUnitWork;

namespace Budget_Man.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _dbCentral;
        public CategoryController(IUnitOfWork db)
        {
            _dbCentral = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _dbCentral.categoryRepository.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            _dbCentral.categoryRepository.Add(obj);
            _dbCentral.Save();
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            _dbCentral.categoryRepository.Update(obj);
            _dbCentral.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            bool flag = _dbCentral.categoryRepository.Remove(id);
            if (flag)
            {
                _dbCentral.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Id " + id + "not found");
            }

        }
    }
}
