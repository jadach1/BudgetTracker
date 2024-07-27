using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Budget_Man.Controllers;
using Budget_Man.Helper.Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{

    // mapper, user by AutoMapper, to map the IdentityUser class to our local UserRegistration model class
    private readonly IMapper _mapper;
    // UserManager is a class maintained by ASP.NET and has many helper methods for handling user submission into DB
    private readonly UserManager<IdentityUser> _userManager;
    public HelperFunctions _helperFunctions;
    
    public AccountController( HelperFunctions helperFunctions,IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _helperFunctions = helperFunctions;
    }

    [HttpGet]
    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(UserRegistration model)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("model state is not valid nor safe");
            return View(model);
        }
        try
        {
            var user = _mapper.Map<IdentityUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code != "DuplicateUserName" && error.Code != "DuplicateEmail")
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }
                    else
                    {
                        ModelState.AddModelError("DuplicateEmail", "Email address already exists.");
                        _helperFunctions.toasterTest("DuplicateEmail",2);
                        break;
                    }
                }
                
                return View(model);
            }
            await _userManager.AddToRoleAsync(user, "Visitor");
            _helperFunctions.toasterTest("Successfully created user.  You may login.",1);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            ViewData["errorMessage"] = "exception " + ex;
            View("Views/Errors/generalError.cshtml");
            return this.Ok($"hello world error");
        }


    }
   
}