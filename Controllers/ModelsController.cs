using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.Models;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/models")]
    public class ModelsController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public ModelsController(VegaDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;

        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairResource>> GetModels()
        {
            var models = await unitOfWork.Models.GetModels();
            return mapper.Map<List<Model>, List<KeyValuePairResource>>(models);
        }
    }
}