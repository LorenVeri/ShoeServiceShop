namespace ShoeService_Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoeServiceDbContext dbContext;

        public UnitOfWork(ShoeServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
