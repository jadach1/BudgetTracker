using Budget_Man.Repository;
using Budget_Man.Server.IRepository;
using Budget_Man.Server.IUnitWork;

namespace Budget_Man.Server.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IFixedExpensesRepository fixedExpensesRepository { get; set; }
        public CategoryRepository categoryRepository { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            fixedExpensesRepository = new FixedExpensesRepository(db);
            categoryRepository = new CategoryRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}