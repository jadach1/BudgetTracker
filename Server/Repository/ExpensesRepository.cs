using Budget_Man.Server.IRepository;
using Budget_Man.Models;
using Budget_Man.Repository.Repository;
using Budget_Man.Server;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Budget_Man.Repository
{
    public class ExpensesRepository : Repository<Expenses>
    {

        private ApplicationDbContext _db;

        public ExpensesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Expenses> GetWeekOf(int week,int month,string userId){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Month == month && e.Week == week && e.MyUserName == userId)
                        .OrderByDescending(s => s.Date)
                        .ToList();
        }

        public async Task<Expenses> GetSingleExpense(int id,string userId){
             IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Id == id && e.MyUserName == userId)
                        .FirstOrDefault();
        }

        public async Task<bool> GetExpensesWithCategoryId(int CategoryId,string userId){
            IQueryable<Expenses> query = dbSet;
           Expenses result = query.Where(e => e.CategoryId == CategoryId && e.MyUserName == userId)
                        .FirstOrDefault();
           
            if(result != null)
                return true;
             else 
                return false;
        }


        public void Update(Expenses obj)
        {
            _db.Expenses.Update(obj);
        }

          public void Update_Change_Expense_Category(int newCategory, int oldCategory, string userid){
            string sql = "'UPDATE [dbo].[Expenses] " + 
                        "SET [CategoryId] ='" + newCategory + "' "+  
                        "WHERE [CategoryId]='"+ oldCategory + "' "+  
                        "AND [Userid]='"+userid+"';";
            _db.Categories.FromSqlRaw(sql); 
        }
    }

}