//Based on index.cshtml in expenses views

using Budget_Man.Models;

public class createExpenseForm {
    //class to hold
    public DisplayExpenses displayExpenses {get; set;}  
    public IEnumerable<Category> categories{ get; set; }
    public string month { get; set; }
    public string type { get; set; }
    public int expense_id {get; set;} 
     
    public createExpenseForm(IEnumerable<Category> categories, string month, string type){
        this.categories = categories;
        this.month = month;
        this.type = type;
        this.expense_id = -1;
    }
}