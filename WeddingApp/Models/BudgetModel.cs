namespace WeddingApp.Models;

public class BudgetModel
{
    public int Id { get; set; }
    public decimal TotalBudget { get; set; }
    public decimal Spent { get; set; }
    public decimal Remaining { get; set; }

    public int WeddingModelId { get; set; }
}