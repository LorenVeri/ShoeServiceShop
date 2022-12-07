using Microsoft.EntityFrameworkCore;
using ShoeService_Data.IRepository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoeService_Common.Constants.SystemConstants;

namespace ShoeService_Data.Repository
{
    public class FunctionRepository : RepositoryBase<Function>, IFunctionRepository
    {
        public FunctionRepository(ShoeServiceDbContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<Function> GetFunctionByRole(string roleId)
        {
            var query = from f in _dbContext.Functions
                        join p in _dbContext.Permissions
                        on f.Id equals p.FunctionId
                        join r in _dbContext.Roles
                        on p.RoleId equals r.Id
                        where r.Id == roleId
                        select f;

            return query;
        }
    }
}
