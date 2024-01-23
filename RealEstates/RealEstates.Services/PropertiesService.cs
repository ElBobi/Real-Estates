using Microsoft.EntityFrameworkCore.Internal;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public class PropertiesService : IPropertiesService
    {
        private RealEstateDbContext db;

        public PropertiesService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public void Create(string district, int size, int? year, int price, string propertyType, string buildingType, int? floor, int? maxFloors)
        {
            var property = new RealEstateProperty
            {
                Size = size,
                Price = price,
                Year = year < 1800 ? null : year,
                Floor = floor,
                TotalNumberOfFloors = maxFloors,
            };

            if (property.Floor <= 0)
            {
                property.Floor = null;
            }

            if (property.TotalNumberOfFloors <= 0)
            {
                property.TotalNumberOfFloors = null;
            }

            // District
            var districtEntity = this.db.Districts.FirstOrDefault(x => x.Name.Trim() == district.Trim());
            if (districtEntity == null)
            {
                districtEntity = new District { Name = district };
            }
            property.District = districtEntity;

            // Property Type
            var propertyTypeEntity = this.db.PropertyTypes.FirstOrDefault(x => x.Name == propertyType.Trim());
            if (propertyTypeEntity == null)
            {
                propertyTypeEntity = new PropertyType { Name = propertyType };
            }
            property.PropertyType = propertyTypeEntity;

            //Building Tyype
            var buildingTypeEntity = this.db.BuildingTypes.FirstOrDefault(x => x.Name.Trim() == buildingType.Trim());
            if (buildingTypeEntity == null)
            {
                buildingTypeEntity = new BuildingType { Name = buildingType };
            }
            property.BuildingType = buildingTypeEntity;

            this.db.RealEstateProperties.Add(property);
            this.db.SaveChanges();

            this.UpdateTags(property.Id);
        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            return db.RealEstateProperties
                .Where(x => x.Year >= minYear && x.Year <= maxYear && x.Size >= minSize && x.Size <= maxSize)
                .Select(MapToPropertyViewModel())
                .OrderBy(x => x.Price)
                .ToList();
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return this.db.RealEstateProperties
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                .Select(MapToPropertyViewModel())
                .OrderBy(x => x.Price)
                .ToList();
        }

        public void UpdateTags(int propertyId)
        {
            throw new NotImplementedException();
        }

        private static Expression<Func<RealEstateProperty, PropertyViewModel>> MapToPropertyViewModel()
        {
            return x => new PropertyViewModel
            {
                Price = x.Price,
                Floor = (x.Floor ?? 0) + "/" + (x.TotalNumberOfFloors ?? 0),
                Size = x.Size,
                Year = x.Year,
                BuildingType = x.BuildingType.Name,
                District = x.District.Name,
                PropertyType = x.PropertyType.Name,
            };
        }
    }
}
