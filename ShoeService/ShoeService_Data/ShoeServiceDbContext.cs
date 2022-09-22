using Microsoft.EntityFrameworkCore;
using ShoeService_Model.Models;

namespace ShoeService_Data
{
    public class ShoeServiceDbContext : DbContext
    {
        public ShoeServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<ShoeRepository> ShoeRepositories { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
    }
}