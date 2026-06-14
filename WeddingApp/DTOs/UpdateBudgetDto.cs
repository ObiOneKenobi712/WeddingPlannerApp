using System.ComponentModel.DataAnnotations;

namespace WeddingApp.DTOs;

public class UpdateBudgetDto
{
    [Range(1, 100000000, ErrorMessage = "Budzet musi byc wiekszy od 0.")]
    public decimal TotalBudget { get; set; }
}

