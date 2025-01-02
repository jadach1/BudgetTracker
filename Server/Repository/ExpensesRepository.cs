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

        //Gets transactions for a particular week
        public  IEnumerable<Expenses> GetWeekOf(int week,int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Year == year && e.Month == month && e.Week == week && e.MyUserName == userId && e.isFixed == false && e.isIncome == false)
                        .OrderByDescending(s => s.Date)
                        .ToList();
        }

        //returns a sum of a week, of a month, of a year
          public async Task<float> GetSumOfWeeksOf(int week, int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return await query.Where(e => e.Year == year && e.Month == month && e.Week == week && e.MyUserName == userId && e.isFixed == false && e.isIncome == false)
                        .SumAsync(t => t.Amount);       
        }

        //returns a sum of all weeks for a month of a specific year
          public async Task<float> GetSumOfAllsWeeksOf(int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return await query.Where(e => e.Year == year && e.Month == month && e.MyUserName == userId && e.isFixed == false && e.isIncome == false)
                        .SumAsync(t => t.Amount);       
        }

        //    public async Task<List<float>> GetSumOfWeeks(int month,int year,string userId){
        //      IQueryable<Expenses> query = dbSet;
        //     var weeks =  await query   .Where(e => e.Year == year && 
        //                                 e.Month == month && 
        //                                 e.isFixed == false && 
        //                                 e.MyUserName == userId)
        //                           .GroupBy(a => a.Week
        //                                   ,a => a.Amount,
        //                                   (key, value) =>  value.Count() > 0 ? value.Sum() : 0)
        //                         .ToListAsync();
            

        //     // if(weeks.Count() == 0)
        //     //   weeks = new List<float>{0,0,0,0,0};
        //     return weeks;
        // }
        
        //Returns Fixed Expenses for a specific month
        public IEnumerable<Expenses> GetFixedExpenses(int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Year == year && e.Month == month && e.isFixed == true && e.MyUserName == userId)
                        .ToList();
        }

        //Returns sum of Fixed Expenses for a specific month
        public async Task<float> GetSumOfFixedExpenses(int month,int year,string userId){
             IQueryable<Expenses> query = dbSet;
            return await query   .Where(e => e.Year == year && 
                               e.Month == month && 
                               e.isFixed == true && 
                               e.MyUserName == userId)
                        .SumAsync(s => s.Amount);
        }

        //Returns Income transactions for a specific month
        public IEnumerable<Expenses> GetIncomeExpenses(int month,int year,string userId){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Year == year && e.Month == month && e.isIncome == true && e.MyUserName == userId)
                        .ToList();
        }

        //Returns sum oif Income transactions for a specific month
        public async Task<float> GetSumOfIncome(int month,int year,string userId){
          IQueryable<Expenses> query = dbSet;
          return await query   .Where(e => e.Year == year && 
                                    e.Month == month && 
                                    e.isIncome == true && 
                                    e.MyUserName == userId)
                                .SumAsync(s => s.Amount);
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

            var result = await _db.Expenses.Where(e => e.CategoryId == oldCategory && e.MyUserName == userid)
                              .ExecuteUpdateAsync(
                                    s => s.SetProperty(e => e.CategoryId, e => newCategory)
                                          .SetProperty(e => e.isFixed, e => false));
            //Check to make sure something happened
            if(result != null){
              return true;
            } else {
              return false;
            }
        }
    
    }

   
}