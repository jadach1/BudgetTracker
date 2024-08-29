using Budget_Man.Models;
using Microsoft.AspNetCore.Mvc;
using Budget_Man.Server.IUnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Budget_Man.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _dbCentral;
        private readonly UserManager<IdentityUser> _userManager;
        public CategoryController(IUnitOfWork db, UserManager<IdentityUser> userManager)
        {
            _dbCentral = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //GET USER 
            IdentityUser user = await GetActiveUser();

            IEnumerable<Category> objCategoryList = _dbCentral.categoryRepository.GetAll(user.Id);
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category obj)
        {
            //GET USER 
            IdentityUser user = await GetActiveUser();
            obj.Userid = user.Id;

            _dbCentral.categoryRepository.Add(obj);
            _dbCentral.Save();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category obj)
        {
              //GET USER 
            IdentityUser user = await GetActiveUser();
            obj.Userid = user.Id;

            _dbCentral.categoryRepository.Update(obj);
            _dbCentral.Save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
              //GET USER 
            IdentityUser user = await GetActiveUser();

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
}
