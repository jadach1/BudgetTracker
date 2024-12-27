using Budget_Man.Server.IRepository;
using Budget_Man.Models;
using Budget_Man.Repository.Repository;
using Budget_Man.Server;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Repository
{
    public class ExpensesRepository : Repository<Expenses>
    {

        private ApplicationDbContext _db;

        public ExpensesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Expenses> GetWeekOf(int week,int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Year == year && e.Month == month && e.Week == week && e.MyUserName == userId && e.isFixed == false && e.isIncome == false)
                        .OrderByDescending(s => s.Date)
                        .ToList();
        }

        public IEnumerable<Expenses> GetFixedExpenses(int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Year == year && e.Month == month && e.isFixed == true && e.MyUserName == userId)
                        .ToList();
        }

        public IEnumerable<Expenses> GetIncomeExpenses(int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Year == year && e.Month == month && e.isIncome == true && e.MyUserName == userId)
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

        // Updates the DB by changing existing expenses to have new categories
          public async Task<bool> Update_Change_Expense_Category(string userid, int oldCategory, int newCategory){
            // string sql = "'UPDATE [dbo].[Expenses] " + 
            //             "SET [CategoryId] ='" + newCategory + "' "+
            //             ", [isFixed] ='" + 0 + "' "+  
            //             ", [Week] ='" + 1 + "' "+
            //             "WHERE [CategoryId]='"+ oldCategory + "' "+  
            //             "AND [Userid]='"+userid+"';";
            // The above is not actually used.
         
            var result = await _db.Expenses.Where(e => e.CategoryId == oldCategory && e.MyUserName == userid)
                              .ExecuteUpdateAsync(
                                    s => s.SetProperty(e => e.CategoryId, e => newCategory)
                                          .SetProperty(e => e.isFixed, e => false)
                                    );
            if(result != null){
              return true;
            } else {
              return false;
            }
        }
     public int dishout(){return 10;}
    
    }

   
}