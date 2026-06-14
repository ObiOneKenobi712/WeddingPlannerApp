using System.ComponentModel.DataAnnotations;

namespace WeddingApp.DTOs;

public class UpdateExpenseDto
{
    [Required(ErrorMessage = "Nazwa wydatku jest wymagana.")]
    [StringLength(100, ErrorMessage = "Nazwa wydatku nie moze przekraczac 100 znakow.")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 10000000, ErrorMessage = "Koszt musi byc wiekszy od 0.")]
    public decimal Cost { get; set; }
}

