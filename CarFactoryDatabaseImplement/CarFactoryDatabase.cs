using CarFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
namespace CarFactoryDatabaseImplement
{
    public class CarFactoryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarFactoryDBlab5;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<WarehouseDetail> WarehouseDetails { set; get; }
        public virtual DbSet<Warehouse> Warehouses { set; get; }
        public virtual DbSet<Detail> Details { set; get; }
        public virtual DbSet<Car> Cars { set; get; }
        public virtual DbSet<CarDetail> CarDetails { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}