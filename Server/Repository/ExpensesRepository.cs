using Budget_Man.Server.IRepository;
using Budget_Man.Models;
using Budget_Man.Repository.Repository;
using Budget_Man.Server;

namespace Budget_Man.Repository
{
    public class ExpensesRepository : Repository<Expenses>
    {

        private ApplicationDbContext _db;

        public ExpensesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Expenses obj)
        {
            _db.Expenses.Update(obj);
        }
    }

}