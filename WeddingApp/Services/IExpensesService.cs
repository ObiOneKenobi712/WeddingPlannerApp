using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public interface IExpensesService
{
    IEnumerable<ExpenseModel> GetAll(int weddingId);
    int Create(int weddingId, CreateExpenseDto dto);
    void Update(int weddingId, int expenseId, UpdateExpenseDto dto);
    void Delete(int weddingId, int expenseId);
}

