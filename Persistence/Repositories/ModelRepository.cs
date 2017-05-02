using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.IRepositories;
using vega.Core.Models;

namespace vega.Persistence.Repositories
{
    public class ModelRepository : Repository<Model>, IModelRepository
    {
        public ModelRepository(VegaDbContext context) : base(context)
        {
        }

        public async Task<Model> GetModel(int id)
        {
            return await context.Models.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Model>> GetModels()
        {
            return await context.Models.ToListAsync();
        }
    }
}