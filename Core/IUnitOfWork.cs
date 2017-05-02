using System;
using System.Threading.Tasks;
using vega.Core.IRepositories;

namespace vega.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IModelRepository Models { get; }
        IMakeRepository Makes { get; }
        IVehicleRepository Vehicles { get; }
        Task CompleteAsync();
    }
}