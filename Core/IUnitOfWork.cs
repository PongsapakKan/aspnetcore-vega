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
        IPhotoRepository Photos { get; }
        Task CompleteAsync();
    }
}