using Budget_Man.Models;

public class MasterOverviewList {
   
   public IEnumerable<MonthlyTransactions> monthlyTransactions {get; set;}

    public MasterOverviewList(){
        this.monthlyTransactions = new MonthlyTransactions[]{};
    }
    
    public void AddNew(IEnumerable<float> weeklyTransactions,float fixedExpenses,float incomeExpenses,string month){
        MonthlyTransactions mt = new MonthlyTransactions(weeklyTransactions,fixedExpenses,incomeExpenses,month);
        monthlyTransactions = monthlyTransactions.Append(mt);
    }
    
}

public class MonthlyTransactions {
    private string Month {get; set;}
    private IEnumerable<float> SumOf_weeklyTransactions;
    private float SumOf_fixedExpenses;
    private float SumOf_incomeExpenses;

     public MonthlyTransactions(IEnumerable<float> weeklyTransactions,float fixedExpenses,float incomeExpenses,string month){
        this.Month = month;
        this.SumOf_weeklyTransactions = weeklyTransactions;
        this.SumOf_fixedExpenses = fixedExpenses;
        this.SumOf_incomeExpenses= incomeExpenses;
    }

    public string getMonth(){ return this.Month;}

    public IEnumerable<float> getWeeklyTransactions(){return this.SumOf_weeklyTransactions;}
    public float getFixedExpenses(){ return this.SumOf_fixedExpenses;}
    public float getIncomeExpenses(){ return this.SumOf_incomeExpenses;}
    public float getTotalWeeklyTransactions() {
        float total = 0;
        foreach(float sum in this.SumOf_weeklyTransactions){
            total += sum;
        }
        return total;
    }
    public float getDifference(){
        return this.getIncomeExpenses() - (this.getFixedExpenses() + this.getTotalWeeklyTransactions());
    }
}