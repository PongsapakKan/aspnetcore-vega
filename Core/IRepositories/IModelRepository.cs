using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core.IRepositories
{
    public interface IModelRepository : IRepository<Model>
    {
        Task<List<Model>> GetModels();
        Task<Model> GetModel(int id);
    }
}