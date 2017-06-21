using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core.IRepositories
{
    public interface IMakeRepository
    {
         Task<List<Make>> GetMakes();
         Task<List<Make>> GetMakesContainId(MakeQuery makeQuery);
         Task<Make> GetMake(int id, bool includeRelated = true);
         void Add(Make make);
         void Remove(Make make);
    }
}