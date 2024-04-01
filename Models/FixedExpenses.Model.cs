using System.ComponentModel.DataAnnotations;
using Budget_Man.Server;

namespace Budget_Man.Models {
    public class FixedExpense {

        private ApplicationDbContext? _db;
        public FixedExpense(ApplicationDbContext db){
            _db = db;
        }

        public FixedExpense(){

        }

        public int Id {get; set;}
        [Required]
        public string? Name {get;set;}
        [Required]
        public float Amount {get;set;}

        public void save(FixedExpense fixedExpense){
            _db.FixedExpense.Add(fixedExpense);
            _db.SaveChanges();
        }
    }
}