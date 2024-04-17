using Budget_Man.Models;

namespace Budget_Man.Server.IRepository{
    public interface IFixedExpensesRepository : IRepository<FixedExpense>{
        void Update(FixedExpense obj);
    }
}