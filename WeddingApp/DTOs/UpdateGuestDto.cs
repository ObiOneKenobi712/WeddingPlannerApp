using System.ComponentModel.DataAnnotations;

namespace WeddingApp.DTOs;

public class UpdateGuestDto
{
    [Required(ErrorMessage = "Imie goscia jest wymagane.")]
    [StringLength(50, ErrorMessage = "Imie goscia nie moze przekraczac 50 znakow.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nazwisko goscia jest wymagane.")]
    [StringLength(50, ErrorMessage = "Nazwisko goscia nie moze przekraczac 50 znakow.")]
    public string LastName { get; set; } = string.Empty;

    public bool IsConfirmed { get; set; }
}

