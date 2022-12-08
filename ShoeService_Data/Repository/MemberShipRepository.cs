using ShoeService_Data.IRepository;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Repository
{
    public class MemberShipRepository : RepositoryBase<MemberShip>, IMemberShipRepository
    {
        public MemberShipRepository(ShoeServiceDbContext dataContext) : base(dataContext)
        {
        }
    }
}
