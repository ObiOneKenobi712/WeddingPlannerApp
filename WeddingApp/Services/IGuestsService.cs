using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public interface IGuestsService
{
    IEnumerable<GuestModel> GetAll(int weddingId);
    GuestModel GetById(int weddingId, int guestId);
    int Create(int weddingId, CreateGuestDto dto);
    void Update(int weddingId, int guestId, UpdateGuestDto dto);
    void Delete(int weddingId, int guestId);
}

