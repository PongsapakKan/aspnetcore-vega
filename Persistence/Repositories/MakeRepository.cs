using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.IRepositories;
using vega.Core.Models;

namespace vega.Persistence.Repositories
{
    public class MakeRepository : Repository<Make>, IMakeRepository
    {
        public MakeRepository(VegaDbContext context) : base(context)
        {
        }

        public async Task<Make> GetMake(int id, bool includeRelated = true)
        {
            if (includeRelated)
                return await context.Makes
                    .Include(m => m.Models)
                    .SingleOrDefaultAsync(m => m.Id == id);

            return await context.Makes.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Make>> GetMakes()
        {
            return await context.Makes
                .Include(m => m.Models)
                .ToListAsync();
        }
    }
}