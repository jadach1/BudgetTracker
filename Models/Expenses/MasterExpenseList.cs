using Budget_Man.Models;

public class MasterExpenseList{
    public createExpenseForm createExpenseForm;
    public Expenses editExpenseform;
    public List<DisplayExpenses> ListOfExpenses;

    public MasterExpenseList(){
        this.ListOfExpenses = new List<DisplayExpenses>();
    }

    public void appendExpenses(IEnumerable<Expenses> expenses,int week,int month,string monthName){
        DisplayExpenses de = new DisplayExpenses(expenses,week,month,monthName);
        ListOfExpenses.Add(de);
    }

    public void appendExpenseForm(IEnumerable<Category> categories, string month, string type){
        this.createExpenseForm = new createExpenseForm(categories, month, type);
    }
}