using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core;
using vega.Core.IRepositories;
using vega.Core.Models;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/models")]
    public class ModelsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IModelRepository repository;
        public ModelsController(IModelRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairResource>> GetModels()
        {
            var models = await repository.GetModels();
            return mapper.Map<List<Model>, List<KeyValuePairResource>>(models);
        }
    }
}