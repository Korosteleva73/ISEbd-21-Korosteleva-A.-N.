using System;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.BindingModels
{
    public class WarehouseBindingModel
    {
        public int? Id { get; set; }

        public string WarehouseName { get; set; }

        public string WarehouseBoss { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> WarehouseDetails { get; set; }

    }
}
