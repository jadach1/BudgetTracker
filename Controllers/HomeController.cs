using Microsoft.AspNetCore.Mvc;
using Budget_Man.AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Budget_Man.Helper.Library;
using Budget_Man.Server.IUnitWork;
using System.Globalization;

namespace Budget_Man.Controllers;

[Authorize]
public class HomeController : Controller
{

    // UserManager is a class maintained by ASP.NET and has many helper methods for handling user submission into DB, and also methods for verifcation
    private readonly UserManager<MyUser> _userManager;
    private readonly SignInManager<MyUser> _signInManager;
    public HelperFunctions _helperFunctions;
    private readonly IUnitOfWork _db;
    
    private readonly ILogger<HomeController> _logger;

    //for converting times and dates to/from int/string
     private DateTimeFormatInfo dtfi;

  public HomeController(IUnitOfWork db, SignInManager<MyUser> signInManager, HelperFunctions helperFunctions, UserManager<MyUser> userManager)
    {
        _userManager = userManager;
        _helperFunctions = helperFunctions;
        _signInManager = signInManager;
        _db = db;
        dtfi = new DateTimeFormatInfo();
    }

    public async Task<IActionResult> Index()
    {
        try {
                //Get user data to display for user
                var name = User.Identity.Name;

                if(name != null){
                    var user = await _userManager.FindByEmailAsync(name);

                    //Set up display data
                    int year = (int)user.year;
                    if(year == null){
                        year = DateTime.Now.Year;
                    }
                    
                    //Create our OverView Model
                    MasterOverviewList masterOverviewList = new MasterOverviewList();
                    
                    //get transactions for each month
                    for(int i = 1;i <= 12; i++){
                        
                        // declare the weekly arrays
                        IEnumerable<float> weeklySum = new List<float>();

                        //get transactions for each week of the month
                        for(int k = 1; k <= 5; k++){
                            //get value
                            float sum = _db.expensesRepository.GetSumOfWeeksOf(k,i, year, user.Id).Result;
                            //convert to 2 dec places
                            sum = (float)Decimal.Round((decimal)sum,2);
                            //set for the overview table, bottom row
                            masterOverviewList.yearlyTotals.addToIndex(sum,k);
                            // month/week rows
                            weeklySum = weeklySum.Append(sum);
                        }

                        //Get month name , as string
                        string month = dtfi.GetMonthName(i);
                        //Get fixed expense sum
                        float FixedSum =  await _db.expensesRepository.GetSumOfFixedExpenses(i,year, user.Id);
                        //Get Income sum
                        float IncomeSum = await _db.expensesRepository.GetSumOfIncome(i,year,user.Id);

                        //Set Yearly Totals
                        masterOverviewList.yearlyTotals.addToIndex(FixedSum,6);
                        masterOverviewList.yearlyTotals.addToIndex(IncomeSum,7);
                        masterOverviewList.yearlyTotals.setAllExpenses();
                        
                        masterOverviewList.AddNew(weeklySum,FixedSum,IncomeSum,month);
                        
                    }
                    return View(masterOverviewList);
                } else {
                    throw new Exception("Could not find user");
                }
        } catch (Exception e) {
            Console.WriteLine("Home Controller exception in Index :",e);
             ViewData["errorMessage"] = "exception " + e;
            return View("Views/Errors/generalError.cshtml");
        }
    }


    public IActionResult Privacy()
    {
        return View();
    }
    
}
