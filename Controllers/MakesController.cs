using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/makes")]
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMake([FromBody] MakeResource makeResource)
        {
             if (!ModelState.IsValid)
                return BadRequest(ModelState);

             var make = mapper.Map<MakeResource, Make>(makeResource);

             context.Makes.Add(make);
             await context.SaveChangesAsync();

             var result = mapper.Map<Make, MakeResource>(make);
             return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] MakeResource makeResource)
        {
             if (!ModelState.IsValid)
                return BadRequest(ModelState);

             var make = await context.Makes.SingleOrDefaultAsync(m => m.Id == id);

             mapper.Map<MakeResource, Make>(makeResource, make);
             
             await context.SaveChangesAsync();

             var result = mapper.Map<Make, MakeResource>(make);
             return Ok(result);
        }
    }
}