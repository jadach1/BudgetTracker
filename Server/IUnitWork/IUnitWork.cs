using Budget_Man.Repository;
using Budget_Man.Server.IRepository;

namespace Budget_Man.Server.IUnitWork {
    public interface IUnitOfWork {
        public IFixedExpensesRepository fixedExpensesRepository { get; set; }
        public CategoryRepository categoryRepository{ get; set; }
        public ExpensesRepository expensesRepository{ get; set; }
        public IUserRepository userRepository {get; set;}
        void Save();
    }
}