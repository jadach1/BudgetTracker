using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Budget_Man.Helper.Library;
using Budget_Man.Server;
using Budget_Man.Server.IUnitWork;
using Budget_Man.Server.UnitWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// we wish to add dbContext to the project, and we need to inform the extension which class in our project will have the dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection2")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
})
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Simply tells the controller instantiating HelperFunctions class that it exists.
builder.Services.AddScoped<HelperFunctions, HelperFunctions>();

builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddScoped<IFixedExpensesRepository,FixedExpensesRepository>();
//Configure login path
    //services.ConfigureApplicationCookie(o => o.LoginPath = "/Authentication/Login");
builder.Services.ConfigureApplicationCookie(o => o.ExpireTimeSpan = System.TimeSpan.FromHours(1));

builder.Services.AddNotyf(config=> { config.DurationInSeconds = 5;config.IsDismissable = true;config.Position = NotyfPosition.BottomCenter; });

//NO MORE SERVICES, BUILD
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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
