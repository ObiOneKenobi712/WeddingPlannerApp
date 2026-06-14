using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public interface IBudgetsService
{
    BudgetModel Get(int weddingId);
    BudgetModel Create(int weddingId, CreateBudgetDto dto);
    BudgetModel Update(int weddingId, UpdateBudgetDto dto);
}

