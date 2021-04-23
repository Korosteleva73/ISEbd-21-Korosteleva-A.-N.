using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarFactoryDatabaseImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Required]
        public string WarehouseName { get; set; }

        [Required]
        public string WarehouseBoss { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual List<WarehouseDetail> WarehouseDetails { get; set; }
    }
}
