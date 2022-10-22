using ShoeService_Data.IRepository;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Repository
{
    public class StorageHasShoesRepository : RepositoryBase<StorageHasShoes>, IStorageHasShoesRepository
    {
        public StorageHasShoesRepository(ShoeServiceDbContext dataContext) : base(dataContext)
        {
        }
    }
}
