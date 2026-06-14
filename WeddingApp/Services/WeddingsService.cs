using Microsoft.EntityFrameworkCore;
using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class WeddingsService : IWeddingsService
{
    private readonly AppDbContext _context;

    public WeddingsService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<WeddingModel> GetAll(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            throw new ApplicationException("Parametry paginacji musza byc wieksze od 0.");
        }

        return _context.Weddings
            .Include(w => w.Guests)
            .Include(w => w.Expenses)
            .Include(w => w.Budget)
            .OrderBy(w => w.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public WeddingModel GetById(int id)
    {
        return _context.Weddings
            .Include(w => w.Guests)
            .Include(w => w.Expenses)
            .Include(w => w.Budget)
            .FirstOrDefault(w => w.Id == id)
            ?? throw new KeyNotFoundException($"Wesele o ID {id} nie istnieje.");
    }

    public int Create(CreateWeddingDto dto)
    {
        if (dto.Date.Date < DateTime.Today)
        {
            throw new ApplicationException("Data wesela nie moze byc z przeszlosci.");
        }

        var normalizedBrideName = dto.BrideName.Trim().ToLower();
        var normalizedGroomName = dto.GroomName.Trim().ToLower();
        var weddingDayStart = dto.Date.Date;
        var weddingDayEnd = weddingDayStart.AddDays(1);

        var exists = _context.Weddings.Any(w =>
            w.BrideName.ToLower() == normalizedBrideName
            && w.GroomName.ToLower() == normalizedGroomName
            && w.Date >= weddingDayStart
            && w.Date < weddingDayEnd);

        if (exists)
        {
            throw new ApplicationException("Takie wesele juz istnieje (ta sama para i data).");
        }

        var wedding = new WeddingModel
        {
            BrideName = dto.BrideName.Trim(),
            GroomName = dto.GroomName.Trim(),
            Date = dto.Date,
            Venue = dto.Venue,
            IsActive = true,
            IsDeleted = false
        };

        _context.Weddings.Add(wedding);
        _context.SaveChanges();

        return wedding.Id;
    }

    public void Update(int id, UpdateWeddingDto dto)
    {
        var wedding = _context.Weddings.FirstOrDefault(w => w.Id == id)
            ?? throw new KeyNotFoundException($"Wesele o ID {id} nie istnieje.");

        if (dto.Date.Date < DateTime.Today)
        {
            throw new ApplicationException("Data wesela nie moze byc z przeszlosci.");
        }

        wedding.Date = dto.Date;
        wedding.Venue = dto.Venue;
        wedding.IsActive = dto.IsActive;

        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var wedding = _context.Weddings
            .Include(w => w.Guests)
            .Include(w => w.Expenses)
            .FirstOrDefault(w => w.Id == id)
            ?? throw new KeyNotFoundException($"Wesele o ID {id} nie istnieje.");

        if (wedding.IsActive)
        {
            throw new ApplicationException("Nie mozna usunac aktywnego wesela. Najpierw ustaw IsActive=false.");
        }

        wedding.IsDeleted = true;
        _context.SaveChanges();
    }
}
