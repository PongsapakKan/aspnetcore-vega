using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/models")]
    public class ModelsController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public ModelsController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }
        
        [HttpGet]
        public async Task<IEnumerable<ModelResource>> GetModels()
        {
            var models = await context.Models.ToListAsync();
            return mapper.Map<List<Model>, List<ModelResource>>(models);
        }
    }
}