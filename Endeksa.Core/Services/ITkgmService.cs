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
        Task<bool> GetDatasByCoordinates(double Latitude, double Longitude);
    }
}
