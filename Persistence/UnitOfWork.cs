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
            Models = new ModelRepository(this.context);
        }

        public IModelRepository Models { get; private set; }

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