namespace WeddingApp.Models;

public class ExpenseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Cost { get; set; }

    public int WeddingModelId { get; set; }
}
