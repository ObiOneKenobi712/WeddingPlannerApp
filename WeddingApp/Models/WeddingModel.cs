namespace WeddingApp.Models;

public class WeddingModel
{
    public int Id { get; set; }
    public string BrideName { get; set; } = "";
    public string GroomName { get; set; } = "";
    public DateTime Date { get; set; }
    public string Venue { get; set; } = "";
    public bool IsActive { get; set; } = true;
    public List<GuestModel> Guests { get; set; } = new();
    public List<ExpenseModel> Expenses { get; set; } = new();
    public BudgetModel? Budget { get; set; }
}

