//Based on index.cshtml in expenses views

using Budget_Man.Models;

public class createExpenseForm {
    public IEnumerable<Category> categories{ get; set; }
    public string month { get; set; }
    public string type { get; set; }
    public Expenses expenseModel { get; set; }
    //to be used in the modal create-expense form
    public List<Expenses> expenseModels { get; set; }
    public createExpenseForm(IEnumerable<Category> categories, string month, string type){
        this.categories = categories;
        this.month = month;
        this.type = type;
        expenseModel = new Expenses();
        expenseModels = new List<Expenses>();
    }
}