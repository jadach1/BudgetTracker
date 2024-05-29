using System.ComponentModel.DataAnnotations.Schema;

namespace Budget_Man.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Week { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        public float Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        // public Expenses(){}

        // public Expenses(int month, int week, int categoryId,float Amount, string date, string description){
        //     // this.Month = Int32.Parse(month);
        //     // this.Week = Int32.Parse(week);
        //     // this.categoryId = Int32.Parse(categoryId);
        //     // this.Amount = Convert.ToSingle(Amount);
        //     this.Month = month;
        //     this.Week = week;   
        //     this.categoryId = categoryId;
        //     this.Amount = Amount;
        //     this.date = date;
        //     this.description = description;
        //     this.category = new Category();
        // }
    }

    public class Expenses2 {
        public string Id { get; set; }
        public string Month { get; set; }
        public string Week { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }

         public static int GetMonthNumber_From_MonthName(string monthname)
        {
            int monthNumber = 0;
            monthNumber= DateTime.ParseExact(monthname, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
            return monthNumber;
        }
    }

   
}