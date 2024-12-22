using Budget_Man.Models;

public class DisplayExpenses {

    public IEnumerable<Expenses> expenses { get; set; }
    
    public int week { get; set; }

    public int month { get; set; }

    public string monthName { get; set; }

    public string uniqueClassNameStart { get; set; }
     public string uniqueClassNameStop { get; set; }

     public string uniqueIdName { get; set; }

     public bool isWeek {get; set;}

    public DisplayExpenses(IEnumerable<Expenses> expenses,int week,int month,string monthName,bool isWeek) {
        this.expenses = expenses;
        this.week = week;
        this.month = month;
        this.monthName = monthName;
       
        /* Checks to see if we are creating a table for a week.  
        // If not, it will be fixed or income transactions.
         They do not need the caret being set up below. */
         
        if(isWeek) {
            this.isWeek = true;
            this.uniqueClassNameStart = "tr-start"+week;
            this.uniqueClassNameStop = "tr-stop"+week;
            this.uniqueIdName = "expensesTableCaretButton"+week;
        } else {
            this.isWeek = false;
        }
        
    }
    
}