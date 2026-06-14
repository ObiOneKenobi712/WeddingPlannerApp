using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class GuestsService : IGuestsService
{
    public IEnumerable<GuestModel> GetAll(int weddingId)
    {
        return GetWeddingOrThrow(weddingId).Guests;
    }

    public GuestModel GetById(int weddingId, int guestId)
    {
        var wedding = GetWeddingOrThrow(weddingId);

        return wedding.Guests.FirstOrDefault(g => g.Id == guestId)
               ?? throw new KeyNotFoundException($"Gosc o ID {guestId} nie istnieje.");
    }

    public int Create(int weddingId, CreateGuestDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);

        var exists = wedding.Guests.Any(g =>
            g.FirstName.ToLower() == dto.FirstName.ToLower() &&
            g.LastName.ToLower() == dto.LastName.ToLower());

        if (exists)
        {
            throw new ApplicationException(
                "Gosc o takim imieniu i nazwisku jest juz zapisany na to wesele.");
        }

        var newId = wedding.Guests.Any()
            ? wedding.Guests.Max(g => g.Id) + 1
            : 1;

        var guest = new GuestModel
        {
            Id = newId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsConfirmed = dto.IsConfirmed
        };

        wedding.Guests.Add(guest);

        return guest.Id;
    }

    public void Update(int weddingId, int guestId, UpdateGuestDto dto)
    {
        var wedding = GetWeddingOrThrow(weddingId);
        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId)
                    ?? throw new KeyNotFoundException($"Gosc o ID {guestId} nie istnieje.");

        guest.FirstName = dto.FirstName;
        guest.LastName = dto.LastName;
        guest.IsConfirmed = dto.IsConfirmed;
    }

    public void Delete(int weddingId, int guestId)
    {
        var wedding = GetWeddingOrThrow(weddingId);
        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId)
                    ?? throw new KeyNotFoundException($"Gosc o ID {guestId} nie istnieje.");

        wedding.Guests.Remove(guest);
    }

    private static WeddingModel GetWeddingOrThrow(int weddingId)
    {
        return WeddingData.Weddings.FirstOrDefault(w => w.Id == weddingId)
               ?? throw new KeyNotFoundException($"Wesele o ID {weddingId} nie istnieje.");
    }
}

