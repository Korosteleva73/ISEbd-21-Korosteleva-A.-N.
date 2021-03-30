using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarFactoryDatabaseImplement.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string CarName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("CarId")]
        public virtual List<CarDetail> CarDetails { get; set; }
        [ForeignKey("CarId")]
        public virtual List<Order> Orders { get; set; }
    }
}
