using System.ComponentModel.DataAnnotations;

namespace WeddingApp.DTOs;

public class CreateWeddingDto
{
    [Required(ErrorMessage = "Imie panny mlodej jest wymagane.")]
    [StringLength(50, ErrorMessage = "Imie panny mlodej nie moze przekraczac 50 znakow.")]
    public string BrideName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Imie pana mlodego jest wymagane.")]
    [StringLength(50, ErrorMessage = "Imie pana mlodego nie moze przekraczac 50 znakow.")]
    public string GroomName { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Miejsce wesela jest wymagane.")]
    [StringLength(100, ErrorMessage = "Miejsce wesela nie moze przekraczac 100 znakow.")]
    public string Venue { get; set; } = string.Empty;
}
