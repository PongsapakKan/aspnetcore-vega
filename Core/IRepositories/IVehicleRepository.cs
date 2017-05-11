using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core.IRepositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
         Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery filter);
         Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
    }
}