using System.Collections.Generic;
using System.Threading.Tasks;

namespace vega.Core.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class 
    {
         void Add(TEntity entity);
         void Remove(TEntity entity);         
    }
}