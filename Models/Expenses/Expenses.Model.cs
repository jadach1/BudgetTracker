using System.ComponentModel.DataAnnotations.Schema;
using Budget_Man.AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace Budget_Man.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public int Year {get; set;}
        public int Month { get; set; }
        public int Week { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        public float Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string MyUserName { get; set; }
        
        [ForeignKey("MyUserName")]
        public MyUser myUser { get; set; }

        public string Currency { get; set; }

        public bool isFixed {get; set;}

        public bool isIncome {get; set;}

    }

    public class ExpensesFormPosting {
        public string Id { get; set; }
        public string Year {get; set;}
        public string Month { get; set; }
        public string Week { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Currency {get; set; }
        public bool isIncome {get; set;}

         public static int GetMonthNumber_From_MonthName(string monthname)
        {
            int monthNumber = 0;
            monthNumber= DateTime.ParseExact(monthname, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
            return monthNumber;
        }
    }

   
}