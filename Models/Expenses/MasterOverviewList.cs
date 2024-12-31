using Budget_Man.Models;

public class MasterOverviewList {
   
   public IEnumerable<MonthlyTransactions> monthlyTransactions {get; set;}

    public YearlyTotals yearlyTotals;
    public MasterOverviewList(){
        this.monthlyTransactions = new MonthlyTransactions[]{};
        this.yearlyTotals = new YearlyTotals();
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

public class YearlyTotals {
    private float TotalforWeek1;
    private float TotalforWeek2;
    private float TotalforWeek3;
    private float TotalforWeek4;
    private float TotalforWeek5;
    private float TotalforExpenses;
    private float TotalforFixedExpenses;
    private float TotalforIncome;
    private float TotalforDifference;

    public YearlyTotals(){
        this.TotalforWeek1 = 0; 
        this.TotalforWeek2=0;
        this.TotalforWeek3=0;
        this.TotalforWeek4=0;
        this.TotalforWeek5=0;
        this.TotalforExpenses=0;
        this.TotalforFixedExpenses=0;
        this.TotalforIncome=0;
        this.TotalforDifference=0;}

    public void addToIndex(float value,int index){
        switch (index){
            case 1:
                this.TotalforWeek1 += value;
                break;
            case 2:
                this.TotalforWeek2 += value;
                break;
            case 3:
                this.TotalforWeek3 += value;
                break;
            case 4:
                this.TotalforWeek4 += value;
                break;
            case 5:
                this.TotalforWeek5 += value;
                break;
            case 6:
                this.TotalforFixedExpenses += value;
                break;
            case 7:
                this.TotalforIncome += value;
                break;
        }
    }

    public void setAllExpenses(){
        this.TotalforExpenses += this.TotalforWeek1 + this.TotalforWeek2 + this.TotalforWeek3 + this.TotalforWeek4 + this.TotalforWeek5;
    }

    // GETTERS
    public float getWeek1(){return this.TotalforWeek1;}
    public float getWeek2(){return this.TotalforWeek2;}
    public float getWeek3(){return this.TotalforWeek3;}
    public float getWeek4(){return this.TotalforWeek4;}
    public float getWeek5(){return this.TotalforWeek5;}

    public float getAllWeeks() {return this.TotalforExpenses;}
    public float getAllFixedExpenses(){return this.TotalforFixedExpenses;}
    public float getAllIncome(){return this.TotalforIncome;} 
    public float getAllDifference(){return this.TotalforIncome - this.TotalforExpenses;}

}