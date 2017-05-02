using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core.IRepositories;

namespace vega.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly VegaDbContext context;
        public Repository(VegaDbContext context)
        {
            this.context = context;
        }
        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }
        public void Remove(TEntity entity)
        {
            context.Remove(entity);
        }
    }
}