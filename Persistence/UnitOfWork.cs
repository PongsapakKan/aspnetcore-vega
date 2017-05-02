using System;
using System.Threading.Tasks;
using vega.Core;
using vega.Core.IRepositories;
using vega.Persistence.Repositories;

namespace vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext context;
        public UnitOfWork(VegaDbContext context)
        {
            this.context = context;
            Makes = new MakeRepository(this.context);            
            Models = new ModelRepository(this.context);
            Vehicles = new VehicleRepository(this.context);
        }

        public IModelRepository Models { get; private set; }
        public IVehicleRepository Vehicles { get; private set; }
        public IMakeRepository Makes { get; private set; }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}