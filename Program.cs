using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Budget_Man.AuthService.Models;
using Budget_Man.Helper.Library;
using Budget_Man.Models;
using Budget_Man.Repository;
using Budget_Man.Server;
using Budget_Man.Server.IRepository;
using Budget_Man.Server.IUnitWork;
using Budget_Man.Server.UnitWork;
using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// we wish to add dbContext to the project, and we need to inform the extension which class in our project will have the dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection2")));

//IDENTITY
builder.Services.AddIdentity<MyUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    // Add token providers
    .AddDefaultTokenProviders();
    //Add security for token
    builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ExpensesRepository, ExpensesRepository>();

//Simply tells the controller instantiating HelperFunctions class that it exists.
builder.Services.AddScoped<HelperFunctions, HelperFunctions>();
//builder.Services.AddScoped<IFixedExpensesRepository,FixedExpensesRepository>();


builder.Services.AddAutoMapper(typeof(Program));

//Configure login path
 //services.ConfigureApplicationCookie(o => o.LoginPath = "/Authentication/Login");

builder.Services.ConfigureApplicationCookie(o => o.ExpireTimeSpan = System.TimeSpan.FromHours(1));

// toast package
builder.Services.AddNotyf(config=> { config.DurationInSeconds = 5;config.IsDismissable = true;config.Position = NotyfPosition.BottomCenter; });

// SMPT email server.  For sending emails when someone registers
var emailConfig = builder.Configuration .GetSection("EmailConfiguration") 
  .Get<EmailConfiguration>(); 
builder.Services.AddSingleton(emailConfig); 
builder.Services.AddScoped<IEmailSender, EmailSender>();

//NO MORE SERVICES, BUILD
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// Enable Identity APIs
// app.MapIdentityApi<User>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
