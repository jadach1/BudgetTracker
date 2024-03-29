using System.ComponentModel.DataAnnotations;

namespace Budget_Man.Models {
    public class FixedExpense {
        public int Id {get; set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public float Amount {get;set;}
    }
}