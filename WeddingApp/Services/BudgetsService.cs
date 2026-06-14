using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class BudgetsService : IBudgetsService
{
    public BudgetModel Get(int weddingId)
    {
        var wedding = GetWeddingOrThrow(weddingId);
        return wedding.Budget ?? throw new KeyNotFoundException("Budzet dla tego wesela nie jest ustawiony.");
    }

    public BudgetModel Create(int weddingId, CreateBudgetDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);

        if (wedding.Budget != null)
        {
            throw new ApplicationException("Budzet dla tego wesela juz istnieje. Uzyj endpointu PUT.");
        }

        wedding.Budget = new BudgetModel
        {
            TotalBudget = dto.TotalBudget,
            Spent = 0,
            Remaining = dto.TotalBudget
        };

        return wedding.Budget;
    }

    public BudgetModel Update(int weddingId, UpdateBudgetDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);

        if (wedding.Budget == null)
        {
            throw new KeyNotFoundException("Budzet dla tego wesela nie jest ustawiony.");
        }

        if (dto.TotalBudget < wedding.Budget.Spent)
        {
            throw new ApplicationException("Nowy budzet nie moze byc mniejszy od aktualnie wydanej kwoty.");
        }

        wedding.Budget.TotalBudget = dto.TotalBudget;
        wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;

        return wedding.Budget;
    }

    private static WeddingModel GetWeddingOrThrow(int weddingId)
    {
        return WeddingData.Weddings.FirstOrDefault(w => w.Id == weddingId)
               ?? throw new KeyNotFoundException($"Wesele o ID {weddingId} nie istnieje.");
    }
}

