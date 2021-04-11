using System.ComponentModel.DataAnnotations;
namespace CarFactoryDatabaseImplement.Models
{
    public class CarDetail
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int DetailId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Detail Detail { get; set; }
        public virtual Car Car { get; set; }
    }
}
