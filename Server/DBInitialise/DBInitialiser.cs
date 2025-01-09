using Budget_Man.AuthService.Models;
using Budget_Man.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class DBInitialiser : IDBInitialiser {
    private readonly UserManager<MyUser> _userManager;
    private readonly SignInManager<MyUser> _signInManager;
    private readonly ApplicationDbContext _db;

    public DBInitialiser(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager, ApplicationDbContext db){
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._db = db;
    }

    public void Initialise(){
        try{
            if(_db.Database.GetPendingMigrations().Count() > 0){
                _db.Database.Migrate();
            }
        } catch (Exception e) {
            
        }
    }
}