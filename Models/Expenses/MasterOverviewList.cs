using Budget_Man.Models;

//The MasteOverviewList class homes the two other classes on this file
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
        return (float)Decimal.Round((decimal)total,2);
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
        this.TotalforExpenses = this.TotalforWeek1 + this.TotalforWeek2 + this.TotalforWeek3 + this.TotalforWeek4 + this.TotalforWeek5;
    }

    // GETTERS
    public decimal getWeek1(){return Decimal.Round((decimal)this.TotalforWeek1,2);}
    public decimal getWeek2(){return Decimal.Round((decimal)this.TotalforWeek2,2);}
    public decimal getWeek3(){return Decimal.Round((decimal)this.TotalforWeek3,2);}
    public decimal getWeek4(){return Decimal.Round((decimal)this.TotalforWeek4,2);}
    public decimal getWeek5(){return Decimal.Round((decimal)this.TotalforWeek5,2);}

    public decimal getAllWeeks() {return Decimal.Round((decimal)this.TotalforExpenses,2);}
    public decimal getAllFixedExpenses(){return Decimal.Round((decimal)this.TotalforFixedExpenses,2);}
    public decimal getAllIncome(){return Decimal.Round((decimal)this.TotalforIncome,2);} 
    public decimal getAllDifference(){
        float sum = this.TotalforIncome -( this.TotalforExpenses + this.TotalforFixedExpenses);
        return Decimal.Round((decimal)sum,2);}

}