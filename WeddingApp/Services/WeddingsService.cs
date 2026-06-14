using WeddingApp.Data;
using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public class WeddingsService : IWeddingsService
{
    public IEnumerable<WeddingModel> GetAll()
    {
        return WeddingData.Weddings;
    }

    public WeddingModel GetById(int id)
    {
        return WeddingData.Weddings.FirstOrDefault(w => w.Id == id)
               ?? throw new KeyNotFoundException($"Wesele o ID {id} nie istnieje.");
    }

    public int Create(CreateWeddingDto dto)
    {
        if (dto.Date.Date < DateTime.Today)
        {
            throw new ApplicationException("Data wesela nie moze byc z przeszlosci.");
        }

        var exists = WeddingData.Weddings.Any(w =>
            string.Equals(w.BrideName, dto.BrideName, StringComparison.OrdinalIgnoreCase)
            && string.Equals(w.GroomName, dto.GroomName, StringComparison.OrdinalIgnoreCase)
            && w.Date.Date == dto.Date.Date);

        if (exists)
        {
            throw new ApplicationException("Takie wesele juz istnieje (ta sama para i data).");
        }

        var newId = WeddingData.Weddings.Any() ? WeddingData.Weddings.Max(w => w.Id) + 1 : 1;

        var wedding = new WeddingModel
        {
            Id = newId,
            BrideName = dto.BrideName,
            GroomName = dto.GroomName,
            Date = dto.Date,
            Venue = dto.Venue,
            IsActive = true
        };

        WeddingData.Weddings.Add(wedding);

        return wedding.Id;
    }

    public void Update(int id, UpdateWeddingDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id)
                      ?? throw new KeyNotFoundException($"Wesele o ID {id} nie istnieje.");

        if (dto.Date.Date < DateTime.Today)
        {
            throw new ApplicationException("Data wesela nie moze byc z przeszlosci.");
        }

        wedding.Date = dto.Date;
        wedding.Venue = dto.Venue;
        wedding.IsActive = dto.IsActive;
    }

    public void Delete(int id)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id)
                      ?? throw new KeyNotFoundException($"Wesele o ID {id} nie istnieje.");

        if (wedding.Guests.Any())
        {
            throw new ApplicationException("Nie można usunąć wesela posiadającego gości.");
        }

        if (wedding.Expenses.Any())
        {
            throw new ApplicationException("Nie można usunąć wesela posiadającego wydatki.");
        }
    }
}

