using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Make>> GetMakesContainId(MakeQuery makeQuery)
        {
            var query = context.Makes
                .Include(m => m.Models)
                .AsQueryable();

            if (makeQuery.Id.Length > 0)
                query = query.Where(m => makeQuery.Id.Contains(m.Id));

            return await query.ToListAsync();
        }

        public async Task<List<Make>> GetMakes()
        {
            return await context.Makes
                .Include(m => m.Models)
                .ToListAsync();
        }
    }
}