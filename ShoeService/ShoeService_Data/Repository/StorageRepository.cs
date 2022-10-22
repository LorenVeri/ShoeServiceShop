
using ShoeService_Data.IRepository;
using ShoeService_Model.Models;

namespace ShoeService_Data.Repository
{
    public class StorageRepository : RepositoryBase<Storage>, IStorageRepository
    {
        public StorageRepository(ShoeServiceDbContext dataContext) : base(dataContext)
        {
        }
    }
}
