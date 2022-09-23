using Microsoft.EntityFrameworkCore;
using ShoeService_Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {

        protected readonly ShoeServiceDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(ShoeServiceDbContext dataContext)
        {
            _dbContext = dataContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            //_dbSet.Attach(entity);
            _dbSet.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual T? GetSingleById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual T? GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return _dbContext.Set<T>().FirstOrDefault(expression);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
    }
}
