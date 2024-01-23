using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public class DistrictsService : IDistrictsService
    {
        public IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10)
        {
            throw new NotImplementedException();
        }
    }
}
