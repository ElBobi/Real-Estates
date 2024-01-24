using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;

namespace RealEstates.ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();
            db.Database.Migrate();

            IPropertiesService propertiesService = new PropertiesService(db);
            propertiesService.Create("Дианабад", 120, 2018, 200000, "4-СТАЕН", "ЕПК", 16, 20);
        }
    }
}