using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class ExpensesService : IExpensesService
{
    public IEnumerable<ExpenseModel> GetAll(int weddingId)
    {
        var wedding = GetWeddingOrThrow(weddingId);
        return wedding.Expenses;
    }

    public int Create(int weddingId, CreateExpenseDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);
        
        if (wedding.Budget != null &&
            wedding.Budget.Spent + dto.Cost > wedding.Budget.TotalBudget)
        {
            throw new ApplicationException(
                "Nie mozna dodac wydatku, bo przekroczylby budzet wesela.");
        }

        var newId = wedding.Expenses.Any()
            ? wedding.Expenses.Max(e => e.Id) + 1
            : 1;

        var expense = new ExpenseModel
        {
            Id = newId,
            Name = dto.Name,
            Cost = dto.Cost
        };

        wedding.Expenses.Add(expense);

        UpdateBudgetAfterExpenseChange(wedding);

        return expense.Id;
    }

    public void Update(int weddingId, int expenseId, UpdateExpenseDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);

        var expense = wedding.Expenses.FirstOrDefault(e => e.Id == expenseId)
            ?? throw new KeyNotFoundException($"Wydatek o ID {expenseId} nie istnieje.");

        var oldCost = expense.Cost;

        // REGUŁA BIZNESOWA:
        // Nie można zaktualizować wydatku, jeśli po zmianie przekroczy budżet.
        if (wedding.Budget != null &&
            wedding.Budget.Spent - oldCost + dto.Cost > wedding.Budget.TotalBudget)
        {
            throw new ApplicationException(
                "Nie mozna zaktualizowac wydatku, bo przekroczylby budzet wesela.");
        }

        expense.Name = dto.Name;
        expense.Cost = dto.Cost;

        UpdateBudgetAfterExpenseChange(wedding);
    }

    public void Delete(int weddingId, int expenseId)
    {
        var wedding = GetWeddingOrThrow(weddingId);

        var expense = wedding.Expenses.FirstOrDefault(e => e.Id == expenseId)
            ?? throw new KeyNotFoundException($"Wydatek o ID {expenseId} nie istnieje.");

        wedding.Expenses.Remove(expense);

        UpdateBudgetAfterExpenseChange(wedding);
    }

    private static WeddingModel GetWeddingOrThrow(int weddingId)
    {
        return WeddingData.Weddings.FirstOrDefault(w => w.Id == weddingId)
            ?? throw new KeyNotFoundException($"Wesele o ID {weddingId} nie istnieje.");
    }

    private static void UpdateBudgetAfterExpenseChange(WeddingModel wedding)
    {
        if (wedding.Budget == null)
        {
            return;
        }

        wedding.Budget.Spent = wedding.Expenses.Sum(e => e.Cost);
        wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;
    }
}