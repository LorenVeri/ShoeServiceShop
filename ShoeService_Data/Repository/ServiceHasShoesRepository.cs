using ShoeService_Data.IRepository;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Repository
{
    public class ServiceHasShoesRepository : RepositoryBase<ServiceHasShoes>, IServiceHasShoesRepository
    {
        public ServiceHasShoesRepository(ShoeServiceDbContext dataContext) : base(dataContext)
        {
        }
    }
}
