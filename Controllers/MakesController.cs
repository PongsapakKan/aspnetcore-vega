using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.Models;

namespace vega.Controllers
{
    [Route("/api/makes")]
    public class MakesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public MakesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await unitOfWork.Makes.GetMakes();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetQueryMakes(MakeQueryResource makrQueryResource)
        {
            var makeQuery = mapper.Map<MakeQueryResource, MakeQuery>(makrQueryResource);
            var makes = await unitOfWork.Makes.GetMakesContainId(makeQuery);
            
            return Ok(mapper.Map<List<Make>, List<MakeResource>>(makes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMake(int id)
        {
            var make = await unitOfWork.Makes.GetMake(id);
            if (make == null)
                return NotFound();

            return Ok(mapper.Map<Make, MakeResource>(make));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMake([FromBody] MakeResource makeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var make = mapper.Map<MakeResource, Make>(makeResource);

            unitOfWork.Makes.Add(make);
            await unitOfWork.CompleteAsync();

            make =  await unitOfWork.Makes.GetMake(make.Id);

            var result = mapper.Map<Make, MakeResource>(make);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] MakeResource makeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var make = await unitOfWork.Makes.GetMake(id);

            mapper.Map<MakeResource, Make>(makeResource, make);
            await unitOfWork.CompleteAsync();

            make =  await unitOfWork.Makes.GetMake(id);

            var result = mapper.Map<Make, MakeResource>(make);
            return Ok(result);
        }

    }
}