using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core.IRepositories
{
    public interface IMakeRepository : IRepository<Make>
    {
         Task<List<Make>> GetMakes();
         Task<Make> GetMake(int id, bool includeRelated = true);
    }
}