using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.IRepositories;
using vega.Core.Models;

namespace vega.Persistence.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(VegaDbContext context) : base(context)
        {
        }

        public async Task<List<Vehicle>> GetVehicles()
        {
            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .ToListAsync();
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}