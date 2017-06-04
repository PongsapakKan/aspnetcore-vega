using System;
using System.Threading.Tasks;
using vega.Core.IRepositories;

namespace vega.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}