using AutoMapper;
using Budget_Man.AuthService.Models;
using Budget_Man.Controllers;
using Budget_Man.Helper.Library;
using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class AccountController : Controller
{

    // mapper, user by AutoMapper, to map the IdentityUser class to our local UserRegistration model class
    private readonly IMapper _mapper;
    // UserManager is a class maintained by ASP.NET and has many helper methods for handling user submission into DB, and also methods for verifcation
    private readonly UserManager<MyUser> _userManager;
    private readonly SignInManager<MyUser> _signInManager;
    private readonly IEmailSender _emailSender;
    public HelperFunctions _helperFunctions;

    public AccountController(SignInManager<MyUser> signInManager, HelperFunctions helperFunctions, IMapper mapper, UserManager<MyUser> userManager, IEmailSender emailSender)
    {
        _mapper = mapper;
        _userManager = userManager;
        _helperFunctions = helperFunctions;
        _signInManager = signInManager;
        _emailSender = emailSender;
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
            Console.WriteLine("model state is not valid nor safe, coming from Register method in AccountController");
            return View(model);
        }
        try
        {
            MyUser user = _mapper.Map<MyUser>(model);
            user.year = DateTime.Now.Year;
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
                        _helperFunctions.toasterTest("Email address already exists", 2);
                        break;
                    }
                }

                return View(model);
            }
            await _userManager.AddToRoleAsync(user, "Visitor");
            _helperFunctions.toasterTest("Successfully created user.  You may login.", 1);
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

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
    {
        Console.WriteLine("user: " + userModel.Email + " mail: " + userModel.Password );
        if (!ModelState.IsValid)
        {
            return View(userModel);
        }

        // Other way of signing in

        // var user = await _userManager.FindByEmailAsync(userModel.Email);
        // if (user != null &&
        //     await _userManager.CheckPasswordAsync(user, userModel.Password))
        // {
        //     var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        //     identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
        //     identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

        //     await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
        //         new ClaimsPrincipal(identity));

        //     return RedirectToLocal(returnUrl);
        // }

        var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
        if (result.Succeeded)
        {
            return RedirectToLocal(returnUrl);
        }
        else
        {
            ModelState.AddModelError("", "Invalid UserName or Password");
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        else
            return RedirectToAction(nameof(HomeController.Index), "Home");

    }

    /*FORGOT PASSWORD*/

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
    {
        if (!ModelState.IsValid)
            return View(forgotPasswordModel);

        var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
        if (user == null)
            return RedirectToAction(nameof(ForgotPasswordConfirmation));

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

        var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
        await _emailSender.SendEmailAsync(message);

        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    /*RESET PASSWORD*/

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        var model = new ResetPasswordModel { Token = token, Email = email };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        if (!ModelState.IsValid)
            return View(resetPasswordModel);

        var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);

        if (user == null)
            RedirectToAction(nameof(ResetPasswordConfirmation));

        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
        if (!resetPassResult.Succeeded)
        {
            foreach (var error in resetPassResult.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return View();
        }

        return RedirectToAction(nameof(ResetPasswordConfirmation));
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

   
}