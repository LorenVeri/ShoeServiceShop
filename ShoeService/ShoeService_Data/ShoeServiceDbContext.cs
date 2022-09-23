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
            modelBuilder.Entity<CustomerHasShoes>().HasKey(table => new
            {
                table.CustomerId,
                table.ShoesId
            });

            modelBuilder.Entity<ServiceHasShoesRepository>().HasKey(table => new
            {
                table.ServiceId,
                table.ShoesRepositoryId
            });

            modelBuilder.Entity<ServiceHasShoes>().HasKey(table => new
            {
                table.ServiceId,
                table.ShoesId
            });

            modelBuilder.Entity<OrderDetail>().HasKey(table => new
            {
                table.OrderId,
                table.ShoesId
            });
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerHasShoes> CustomerHasShoes { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceHasShoesRepository> ServiceHasShoesRepository { get; set; }
        public DbSet<ServiceHasShoes> ServiceHasShoes { get; set; }
        public DbSet<Shoes> Shoes { get; set; }
        public DbSet<ShoesRepository> ShoeRepositories { get; set; }
    }
}