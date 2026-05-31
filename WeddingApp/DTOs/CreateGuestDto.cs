namespace WeddingApp.DTOs;

public class CreateGuestDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public bool IsConfirmed { get; set; } = false;
}