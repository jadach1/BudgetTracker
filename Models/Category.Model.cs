using System.ComponentModel.DataAnnotations;

namespace Budget_Man.Models {
    public class Category {
        public int Id {get; set;}
        [Required]
        public string Name {get;set;}

        public string Userid {get;set;}
    }
}