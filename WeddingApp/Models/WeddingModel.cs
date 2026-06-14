namespace WeddingApp.Models;

public class WeddingModel
{
    public int Id { get; set; }
    public string BrideName { get; set; } = string.Empty;
    public string GroomName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Venue { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public List<GuestModel> Guests { get; set; } = new();
    public List<ExpenseModel> Expenses { get; set; } = new();
    public BudgetModel? Budget { get; set; }
}
