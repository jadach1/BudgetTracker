using System.ComponentModel.DataAnnotations;
using Budget_Man.Server;

namespace Budget_Man.Models {
    public class FixedExpense {

        public int Id {get; set;}
        [Required]
        public string? Name {get;set;}
        [Required]
        public float? Amount {get;set;}
        [Required]
        public string? Currency   {get;set;}
         public string Userid {get;set;}

        private ApplicationDbContext? _db;
          public FixedExpense(){
            Name = "";
            Amount = 0;
            Currency = "USD";
            Userid = "";
        }

        public FixedExpense(ApplicationDbContext db){
            _db = db;
        }

        public void save(FixedExpense fixedExpense){
            _db.FixedExpense.Add(fixedExpense);
            _db.SaveChanges();
        }
    }
}