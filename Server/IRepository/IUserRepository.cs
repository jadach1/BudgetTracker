using Budget_Man.Migrations;
using Budget_Man.Models;

namespace Budget_Man.Server.IRepository{
    public interface IUserRepository {
         Task<int> ChangeYear(string userid,int year);
    }
}