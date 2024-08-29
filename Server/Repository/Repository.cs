using System.Linq.Expressions;
using Budget_Man.Server;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Repository.Repository
{
    public class Repository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public bool Remove(int id)
        {
            if (id != 0)
            {
                T obj = dbSet.Find([id]);
                _db.Remove(obj);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}