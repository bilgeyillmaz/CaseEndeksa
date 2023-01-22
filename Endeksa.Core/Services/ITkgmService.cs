using Endeksa.Core.DTOs;
using Endeksa.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Services
{
    public interface ITkgmService : IService<Root>
    {
        Task<RootDto> GetDatasByCoordinates(double Latitude, double Longitude);
        Task<CityFeature> GetOrCreateCityByIdAsync(int CityId);
        Task<DistrictRootObject> GetDistricts(int CityId);
        Task<NeighborhoodRootObject> GetNeighborhoods(int DistrictId);
    }
}
