using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var db = new RealEstateDbContext();
            db.Database.Migrate();
        }
    }
}