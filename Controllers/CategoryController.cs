using Budget_Man.Models;
using Microsoft.AspNetCore.Mvc;
using Budget_Man.Server.IUnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Budget_Man.Helper.Library;
using Budget_Man.AuthService.Models;

namespace Budget_Man.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _dbCentral;
        private readonly UserManager<MyUser> _userManager;
        private HelperFunctions _helperFunctions;
        public CategoryController(IUnitOfWork db, UserManager<MyUser> userManager,HelperFunctions helperFunctions)
        {
            _dbCentral = db;
            _userManager = userManager;
            _helperFunctions = helperFunctions;
        }

        public async Task<IActionResult> Index()
        {
            //GET USER 
            IdentityUser user = await GetActiveUser();

            IEnumerable<Category> objCategoryList = _dbCentral.categoryRepository.GetAll(user.Id);
            return View(objCategoryList);
        }

        public async Task<int> CheckIfCategoryExists(){
            //GET USER 
            IdentityUser user = await GetActiveUser();
            var obj = await _dbCentral.categoryRepository.CheckIfCategoryExists("Fixed Expense", user.Id);
            return obj;
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
            _helperFunctions.toasterTest("New Category Created",1);
            return View();
        }

        public async Task<int> CreateCategory(){
            //GET USER 
            IdentityUser user = await GetActiveUser();
            var obj = new Category();
            obj.Userid = user.Id;
            obj.Name = "Fixed Expense";

            _dbCentral.categoryRepository.Add(obj);
            _dbCentral.Save();

            _helperFunctions.toasterTest("New Category Created",1);
            return obj.Id;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category obj)
        {
              //GET USER 
            IdentityUser user = await GetActiveUser();
            obj.Userid = user.Id;

            _dbCentral.categoryRepository.Update(obj);
            _dbCentral.Save();
            _helperFunctions.toasterTest("Category Edit Successfully",3);
            return RedirectToAction("Index");
        }

        public async Task<bool> Delete(int id)
        {
              //GET USER 
            IdentityUser user = await GetActiveUser();
            bool flag = _dbCentral.categoryRepository.Remove(id);
            if (flag)
            {
                _dbCentral.Save();
                _helperFunctions.toasterTest("Category Deleted",2);
                return true;
            }
            else
            {
                return false;
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
