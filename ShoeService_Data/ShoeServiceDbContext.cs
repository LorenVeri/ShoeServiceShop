using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;

namespace ShoeService_Data
{
    public class ShoeServiceDbContext : IdentityDbContext<User>
    {
        public ShoeServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<OrderDetail>().HasKey(table => new
            {
                table.OrderId,
                table.ServiceId
            });

            modelBuilder.Entity<ServiceHasShoes>().HasKey(table => new
            {
                table.ShoesId,
                table.ServiceId
            });

            modelBuilder.Entity<ServiceHasStorage>().HasKey(table => new
            {
                table.StorageId,
                table.ServiceId
            });

            modelBuilder.Entity<StorageHasShoes>().HasKey(table => new
            {
                table.StorageId,
                table.ShoesId
            });

            modelBuilder.Entity<Permission>().HasKey(c => new 
            { 
                c.RoleId, 
                c.FunctionId, 
                c.CommandId 
            });

            modelBuilder.Entity<CommandInFunction>()
                       .HasKey(c => new { c.CommandId, c.FunctionId });

            modelBuilder.Entity<Customer>()
                .HasMany(p => p.Shoes)
                .WithOne(b => b.Customer)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("ForeignKey_Customer_Shoes")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MemberShip>()
                .HasMany(p => p.Customers)
                .WithOne(b => b.MemberShip)
                .HasForeignKey(p => p.MemberShipId)
                .HasConstraintName("ForeignKey_MemberShip_Customer");

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(b => b.Product)
                .HasForeignKey(p => p.ProductId)
                .HasConstraintName("ForeignKey_Product_ProductImage")
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceHasShoes> ServiceHasShoes { get; set; }
        public DbSet<ServiceHasStorage> ServiceHasRepositories { get; set; }
        public DbSet<Shoes> Shoes { get; set; }
        public DbSet<Storage> Repositories { get; set; }
        public DbSet<StorageHasShoes> StorageHasShoes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<CommandInFunction> CommandInFunctions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}