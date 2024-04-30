using System.ComponentModel.DataAnnotations.Schema;

namespace Budget_Man.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public Category category { get; set; }
        public float Amount { get; set; }
        public DateOnly date { get; set; }
        public string description { get; set; }

    }
}