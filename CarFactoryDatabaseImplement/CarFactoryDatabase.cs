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
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarFactoryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Detail> Details { set; get; }
        public virtual DbSet<Car> Cars { set; get; }
        public virtual DbSet<CarDetail> CarDetails { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
    }
}