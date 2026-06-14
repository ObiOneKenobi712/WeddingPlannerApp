using System.ComponentModel.DataAnnotations;

namespace WeddingApp.DTOs;

public class UpdateWeddingDto
{
    [Required(ErrorMessage = "Miejsce wesela jest wymagane.")]
    [StringLength(100, ErrorMessage = "Miejsce wesela nie moze przekraczac 100 znakow.")]
    public string Venue { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public bool IsActive { get; set; }
}

