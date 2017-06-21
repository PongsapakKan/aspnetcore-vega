using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.IRepositories;
using vega.Core.Models;

namespace vega.Persistence.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly VegaDbContext context;
        public ModelRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<Model> GetModel(int id)
        {
            return await context.Models.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Model>> GetModels()
        {
            return await context.Models.ToListAsync();
        }

        public void Add(Model model)
        {
            context.Models.Add(model);
        }

        public void Remove(Model model)
        {
            context.Remove(model);
        }
    }
}