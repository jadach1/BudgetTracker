using AutoMapper;
using Budget_Man.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
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
                    } else {
                        ModelState.AddModelError("DuplicateEmail","Email address already exists.");
                    }
                }
                return View(model);
            }
            await _userManager.AddToRoleAsync(user, "Visitor");
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