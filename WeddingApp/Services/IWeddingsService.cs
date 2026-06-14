using WeddingApp.DTOs;
using WeddingApp.Models;

namespace WeddingApp.Services;

public interface IWeddingsService
{
    IEnumerable<WeddingModel> GetAll();
    WeddingModel GetById(int id);
    int Create(CreateWeddingDto dto);
    void Update(int id, UpdateWeddingDto dto);
    void Delete(int id);
}

