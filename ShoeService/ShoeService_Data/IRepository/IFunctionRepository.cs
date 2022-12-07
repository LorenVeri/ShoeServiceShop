using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.IRepository
{
    public interface IFunctionRepository : IRepository<Function>
    {
        IQueryable<Function> GetFunctionByRole(string roleId);
    }
}
