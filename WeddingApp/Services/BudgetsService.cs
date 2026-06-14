using Microsoft.EntityFrameworkCore;
using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class BudgetsService : IBudgetsService
{
    private readonly AppDbContext _context;

    public BudgetsService(AppDbContext context)
    {
        _context = context;
    }

    public BudgetModel Get(int weddingId)
    {
        EnsureWeddingExists(weddingId);

        return _context.Budgets.FirstOrDefault(b => b.WeddingModelId == weddingId)
               ?? throw new KeyNotFoundException("Budzet dla tego wesela nie jest ustawiony.");
    }

    public BudgetModel Create(int weddingId, CreateBudgetDto dto)
    {
        EnsureWeddingExists(weddingId);

        var existing = _context.Budgets.FirstOrDefault(b => b.WeddingModelId == weddingId);
        if (existing != null)
        {
            throw new ApplicationException("Budzet dla tego wesela juz istnieje. Uzyj endpointu PUT.");
        }

        var budget = new BudgetModel
        {
            WeddingModelId = weddingId,
            TotalBudget = dto.TotalBudget,
            Spent = 0,
            Remaining = dto.TotalBudget
        };

        _context.Budgets.Add(budget);
        _context.SaveChanges();

        return budget;
    }

    public BudgetModel Update(int weddingId, UpdateBudgetDto dto)
    {
        EnsureWeddingExists(weddingId);

        var budget = _context.Budgets.FirstOrDefault(b => b.WeddingModelId == weddingId)
                     ?? throw new KeyNotFoundException("Budzet dla tego wesela nie jest ustawiony.");

        if (dto.TotalBudget < budget.Spent)
        {
            throw new ApplicationException("Nowy budzet nie moze byc mniejszy od aktualnie wydanej kwoty.");
        }

        budget.TotalBudget = dto.TotalBudget;
        budget.Remaining = budget.TotalBudget - budget.Spent;

        _context.SaveChanges();

        return budget;
    }

    private void EnsureWeddingExists(int weddingId)
    {
        var exists = _context.Weddings.AsNoTracking().Any(w => w.Id == weddingId);
        if (!exists)
        {
            throw new KeyNotFoundException($"Wesele o ID {weddingId} nie istnieje.");
        }
    }
}
