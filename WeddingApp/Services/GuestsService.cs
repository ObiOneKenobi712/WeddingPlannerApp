using Microsoft.EntityFrameworkCore;
using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class GuestsService : IGuestsService
{
    private readonly AppDbContext _context;

    public GuestsService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<GuestModel> GetAll(int weddingId)
    {
        EnsureWeddingExists(weddingId);

        return _context.Guests
            .Where(g => g.WeddingModelId == weddingId)
            .OrderBy(g => g.Id)
            .ToList();
    }

    public GuestModel GetById(int weddingId, int guestId)
    {
        EnsureWeddingExists(weddingId);

        return _context.Guests.FirstOrDefault(g => g.WeddingModelId == weddingId && g.Id == guestId)
               ?? throw new KeyNotFoundException($"Gosc o ID {guestId} nie istnieje.");
    }

    public int Create(int weddingId, CreateGuestDto dto)
    {
        EnsureWeddingExists(weddingId);

        var exists = _context.Guests.Any(g =>
            g.WeddingModelId == weddingId &&
            g.FirstName.ToLower() == dto.FirstName.ToLower() &&
            g.LastName.ToLower() == dto.LastName.ToLower());

        if (exists)
        {
            throw new ApplicationException("Gosc o takim imieniu i nazwisku jest juz zapisany na to wesele.");
        }

        var guest = new GuestModel
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsConfirmed = dto.IsConfirmed,
            WeddingModelId = weddingId
        };

        _context.Guests.Add(guest);
        _context.SaveChanges();

        return guest.Id;
    }

    public void Update(int weddingId, int guestId, UpdateGuestDto dto)
    {
        EnsureWeddingExists(weddingId);

        var guest = _context.Guests.FirstOrDefault(g =>
                        g.WeddingModelId == weddingId &&
                        g.Id == guestId)
                    ?? throw new KeyNotFoundException(
                        $"Gosc o ID {guestId} nie istnieje.");

        // REGUŁA BIZNESOWA:
        // Nie można zmienić gościa tak, aby powstał duplikat.
        var exists = _context.Guests.Any(g =>
            g.WeddingModelId == weddingId &&
            g.Id != guestId &&
            g.FirstName.ToLower() == dto.FirstName.ToLower() &&
            g.LastName.ToLower() == dto.LastName.ToLower());

        if (exists)
        {
            throw new ApplicationException(
                "Gosc o takim imieniu i nazwisku jest juz zapisany na to wesele.");
        }

        guest.FirstName = dto.FirstName;
        guest.LastName = dto.LastName;
        guest.IsConfirmed = dto.IsConfirmed;

        _context.SaveChanges();
    }

    public void Delete(int weddingId, int guestId)
    {
        EnsureWeddingExists(weddingId);

        var guest = _context.Guests.FirstOrDefault(g => g.WeddingModelId == weddingId && g.Id == guestId)
                    ?? throw new KeyNotFoundException($"Gosc o ID {guestId} nie istnieje.");

        _context.Guests.Remove(guest);
        _context.SaveChanges();
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
