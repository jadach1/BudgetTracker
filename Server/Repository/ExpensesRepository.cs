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

        public IEnumerable<Expenses> GetWeekOf(int week,int month){
            IQueryable<Expenses> query = dbSet;
            return query.Where(e => e.Month == month && e.Week == week).OrderByDescending(s => s.Date).ToList();
        }

        public void Update(Expenses obj)
        {
            _db.Expenses.Update(obj);
        }
    }

}