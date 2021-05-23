using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string WarehouseName { get; set; }

        [DisplayName("Ответственный")]
        public string WarehouseBoss { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> WarehouseDetails { get; set; }
    }
}
