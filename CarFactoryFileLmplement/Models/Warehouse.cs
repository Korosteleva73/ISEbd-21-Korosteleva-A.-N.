﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarFactoryFileImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public string WarehouseBoss { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, int> WarehouseDetails { get; set; }
    }
}
