namespace WeddingApp.Models;

public class GuestModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; } = false;

    public int WeddingModelId { get; set; }
}