
namespace ShoeService_Data.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        void Commit();
    }
}
