using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class ExpensesService : IExpensesService
{
    public IEnumerable<ExpenseModel> GetAll(int weddingId)
    {
        return GetWeddingOrThrow(weddingId).Expenses;
    }

    public int Create(int weddingId, CreateExpenseDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);

        if (wedding.Budget != null && wedding.Budget.Spent + dto.Cost > wedding.Budget.TotalBudget)
        {
            throw new ApplicationException("Nie mozna dodac wydatku, bo przekroczylby budzet wesela.");
        }

        var newId = wedding.Expenses.Any() ? wedding.Expenses.Max(e => e.Id) + 1 : 1;
        var expense = new ExpenseModel
        {
            Id = newId,
            Name = dto.Name,
            Cost = dto.Cost
        };

        wedding.Expenses.Add(expense);

        if (wedding.Budget != null)
        {
            wedding.Budget.Spent += expense.Cost;
            wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;
        }

        return expense.Id;
    }

    public void Update(int weddingId, int expenseId, UpdateExpenseDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);
        var expense = wedding.Expenses.FirstOrDefault(e => e.Id == expenseId)
                     ?? throw new KeyNotFoundException($"Wydatek o ID {expenseId} nie istnieje.");

        var oldCost = expense.Cost;

        if (wedding.Budget != null && wedding.Budget.Spent - oldCost + dto.Cost > wedding.Budget.TotalBudget)
        {
            throw new ApplicationException("Nie mozna zaktualizowac wydatku, bo przekroczylby budzet wesela.");
        }

        expense.Name = dto.Name;
        expense.Cost = dto.Cost;

        if (wedding.Budget != null)
        {
            wedding.Budget.Spent = wedding.Budget.Spent - oldCost + expense.Cost;
            wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;
        }
    }

    public void Delete(int weddingId, int expenseId)
    {
        var wedding = GetWeddingOrThrow(weddingId);
        var expense = wedding.Expenses.FirstOrDefault(e => e.Id == expenseId)
                     ?? throw new KeyNotFoundException($"Wydatek o ID {expenseId} nie istnieje.");

        if (wedding.Budget != null)
        {
            wedding.Budget.Spent -= expense.Cost;
            wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;
        }

        wedding.Expenses.Remove(expense);
    }

    private static WeddingModel GetWeddingOrThrow(int weddingId)
    {
        return WeddingData.Weddings.FirstOrDefault(w => w.Id == weddingId)
               ?? throw new KeyNotFoundException($"Wesele o ID {weddingId} nie istnieje.");
    }
}

