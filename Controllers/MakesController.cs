using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.IRepositories;
using vega.Core.Models;

namespace vega.Controllers
{
    [Route("/api/makes")]
    public class MakesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMakeRepository repository;
        public MakesController(IMapper mapper, IMakeRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await repository.GetMakes();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetQueryMakes(MakeQueryResource makrQueryResource)
        {
            var makeQuery = mapper.Map<MakeQueryResource, MakeQuery>(makrQueryResource);
            var makes = await repository.GetMakesContainId(makeQuery);

            return Ok(mapper.Map<List<Make>, List<MakeResource>>(makes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMake(int id)
        {
            var make = await repository.GetMake(id);
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

            repository.Add(make);
            await unitOfWork.CompleteAsync();

            make = await repository.GetMake(make.Id);

            var result = mapper.Map<Make, MakeResource>(make);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMake(int id, [FromBody] MakeResource makeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var make = await repository.GetMake(id);

            mapper.Map<MakeResource, Make>(makeResource, make);
            await unitOfWork.CompleteAsync();

            make = await repository.GetMake(id);

            var result = mapper.Map<Make, MakeResource>(make);
            return Ok(result);
        }

    }
}