using Budget_Man.Server.IRepository;
using Budget_Man.Models;
using Budget_Man.Repository.Repository;
using Budget_Man.Server;

namespace Budget_Man.Repository
{
    public class FixedExpensesRepository : Repository<FixedExpense>, IFixedExpensesRepository
    {

        private  ApplicationDbContext _db;

        public FixedExpensesRepository(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        public void Update(FixedExpense obj)
        {
           _db.FixedExpense.Update(obj);
        }

        public IEnumerable<FixedExpense> GetAll(string userid){
            IQueryable<FixedExpense> query = dbSet;
            return query.Where(e => e.Userid == userid )
                        .ToList();
        }
    }
}