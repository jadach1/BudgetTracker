using Budget_Man.Server.IRepository;
using Budget_Man.Server;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Repository
{
    public class UserRepository : IUserRepository
    {

        private  ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)  {
            _db = db;
        }

        public async Task<int> ChangeCurrency(string userid, string currency)
        {
           var result = await _db.User.Where(e => e.Id == userid)
                              .ExecuteUpdateAsync(
                                    s => s.SetProperty(e => e.currency, e => currency));
          return result;
        }

        public async Task<int> ChangeYear(string userid,int year){
         var result = await _db.User.Where(e => e.Id == userid)
                              .ExecuteUpdateAsync(
                                    s => s.SetProperty(e => e.year, e => year));
          return result;
      }
    }
}