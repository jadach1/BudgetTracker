using System.Linq.Expressions;

namespace Budget_Man.Server.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        Task<T> Get(Expression<Func<T,bool>>filter);
        IEnumerable<T> GetAll();
        bool Remove(int id); 
    }
}