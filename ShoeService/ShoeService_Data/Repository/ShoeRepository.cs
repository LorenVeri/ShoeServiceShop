using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Repository
{
    public class ShoeRepository : RepositoryBase<Shoe>, IShoeRepository
    {
        private readonly ShoeServiceDbContext _dbContext;
        public ShoeRepository(ShoeServiceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
