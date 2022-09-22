using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.IRepository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        T GetSingleById(Guid id);
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);
        Task<int> SaveAsync();
    }
}
