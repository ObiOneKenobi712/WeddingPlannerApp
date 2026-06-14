using Microsoft.EntityFrameworkCore;
using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class ExpensesService : IExpensesService
{
    private readonly AppDbContext _context;

    public ExpensesService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ExpenseModel> GetAll(int weddingId)
    {
        EnsureWeddingExists(weddingId);

        return _context.Expenses
            .Where(e => e.WeddingModelId == weddingId)
            .OrderBy(e => e.Id)
            .ToList();
    }

    public int Create(int weddingId, CreateExpenseDto dto)
    {
        var wedding = GetWeddingWithBudgetOrThrow(weddingId);

        if (wedding.Budget != null && wedding.Budget.Spent + dto.Cost > wedding.Budget.TotalBudget)
        {
            throw new ApplicationException("Nie mozna dodac wydatku, bo przekroczylby budzet wesela.");
        }

        var expense = new ExpenseModel
        {
            Name = dto.Name,
            Cost = dto.Cost,
            WeddingModelId = weddingId
        };

        _context.Expenses.Add(expense);
        _context.SaveChanges();

        RecalculateBudget(weddingId);

        return expense.Id;
    }

    public void Update(int weddingId, int expenseId, UpdateExpenseDto dto)
    {
        var wedding = GetWeddingWithBudgetOrThrow(weddingId);

        var expense = _context.Expenses.FirstOrDefault(e => e.WeddingModelId == weddingId && e.Id == expenseId)
            ?? throw new KeyNotFoundException($"Wydatek o ID {expenseId} nie istnieje.");

        var oldCost = expense.Cost;

        if (wedding.Budget != null && wedding.Budget.Spent - oldCost + dto.Cost > wedding.Budget.TotalBudget)
        {
            throw new ApplicationException("Nie mozna zaktualizowac wydatku, bo przekroczylby budzet wesela.");
        }

        expense.Name = dto.Name;
        expense.Cost = dto.Cost;

        _context.SaveChanges();

        RecalculateBudget(weddingId);
    }

    public void Delete(int weddingId, int expenseId)
    {
        EnsureWeddingExists(weddingId);

        var expense = _context.Expenses.FirstOrDefault(e => e.WeddingModelId == weddingId && e.Id == expenseId)
            ?? throw new KeyNotFoundException($"Wydatek o ID {expenseId} nie istnieje.");

        _context.Expenses.Remove(expense);
        _context.SaveChanges();

        RecalculateBudget(weddingId);
    }

    private WeddingModel GetWeddingWithBudgetOrThrow(int weddingId)
    {
        return _context.Weddings
            .Include(w => w.Budget)
            .FirstOrDefault(w => w.Id == weddingId)
            ?? throw new KeyNotFoundException($"Wesele o ID {weddingId} nie istnieje.");
    }

    private void EnsureWeddingExists(int weddingId)
    {
        var exists = _context.Weddings.AsNoTracking().Any(w => w.Id == weddingId);
        if (!exists)
        {
            throw new KeyNotFoundException($"Wesele o ID {weddingId} nie istnieje.");
        }
    }

    private void RecalculateBudget(int weddingId)
    {
        var budget = _context.Budgets.FirstOrDefault(b => b.WeddingModelId == weddingId);
        if (budget == null)
        {
            return;
        }

        var spent = _context.Expenses
            .Where(e => e.WeddingModelId == weddingId)
            .Sum(e => e.Cost);

        budget.Spent = spent;
        budget.Remaining = budget.TotalBudget - budget.Spent;
        _context.SaveChanges();
    }
}