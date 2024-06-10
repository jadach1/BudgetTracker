//Based on index.cshtml in expenses views

using Budget_Man.Models;

public class createExpenseForm {
    public IEnumerable<Category> categories{ get; set; }
    public string month { get; set; }
    public string type { get; set; }
    public Expenses expenseModel { get; set; }
    //to be used in the modal create-expense form
    public IEnumerable<Expenses> expenses { get; set; }
    public createExpenseForm(IEnumerable<Expenses> expenses,IEnumerable<Category> categories, string month, string type){
        this.expenses = expenses;
        this.categories = categories;
        this.month = month;
        this.type = type;
        expenseModel = new Expenses();
    }
}