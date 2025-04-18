using Budget_Man.Models;
using Budget_Man.Repository.Repository;
using Budget_Man.Server;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Repository {
    public class CategoryRepository : Repository<Category>
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj){
            _db.Categories.Update(obj);    
        }

        public IEnumerable<Category> GetAll(string userid){
            IQueryable<Category> query = dbSet;
            return query.Where(e => e.Userid == userid )
                        .ToList();
        }

        public async Task<int> CheckIfCategoryExists(string categoryName,string userid){
            IQueryable<Category> query = dbSet;
            var obj = query.Where(e => e.Name == categoryName && e.Userid == userid ).FirstOrDefault();
            if (obj == null){  
                Console.WriteLine("There is no category here");
                return 0;
            }
            else {
                return obj.Id;
            }
        }      
        }
    }