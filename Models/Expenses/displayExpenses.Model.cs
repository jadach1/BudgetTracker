using Budget_Man.Models;

public class DisplayExpenses {

    public IEnumerable<Expenses> expenses { get; set; }
    
    public int week { get; set; }

    public int month { get; set; }

    public string monthName { get; set; }

    public string uniqueClassNameStart { get; set; }
     public string uniqueClassNameStop { get; set; }

     public string uniqueIdName { get; set; }

    public DisplayExpenses(IEnumerable<Expenses> expenses,int week,int month,string monthName) {
        this.expenses = expenses;
        this.week = week;
        this.month = month;
        this.monthName = monthName;
        this.uniqueClassNameStart = "tr-start"+week;
        this.uniqueClassNameStop = "tr-stop"+week;
        this.uniqueIdName = "expensesTableCaretButton"+week;
    }
    
}