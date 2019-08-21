using IncognitusBack.Core.Interfaces;
using IncognitusBack.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncognitusBack.DataAccess.Data
{
    public class EfRepositoryViews<T> : IAsyncRepositoryNormal<T> where T: BaseNotKey
    {
        protected readonly IncogDbContext _dbContext;

        public EfRepositoryViews(IncogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Query<T>().ToListAsync();
        }
    }
}
