using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core.IRepositories
{
    public interface IModelRepository
    {
        Task<List<Model>> GetModels();
        Task<Model> GetModel(int id);
        void Add(Model model);
        void Remove(Model model);
    }
}