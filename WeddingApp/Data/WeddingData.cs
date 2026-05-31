using WeddingApp.Models;

namespace WeddingApp.Data;

public static class WeddingData
{
    public static List<WeddingModel> Weddings = new()
    {
        new WeddingModel
        {
            Id = 1,
            BrideName = "Anna",
            GroomName = "Jan",
            Date = new DateTime(2027, 6, 15),
            Venue = "Hotel Victoria",
            IsActive = true
        },
        new WeddingModel
        {
            Id = 2,
            BrideName = "Maria",
            GroomName = "Piotr",
            Date = new DateTime(2027, 8, 20),
            Venue = "Pałac Jabłonna",
            IsActive = true
        }
    };
}
