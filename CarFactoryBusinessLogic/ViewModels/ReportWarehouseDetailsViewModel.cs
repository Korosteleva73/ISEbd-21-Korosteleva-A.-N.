using System;
using System.Collections.Generic;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class ReportWarehouseDetailsViewModel
    {
        public string WarehouseName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Details { get; set; }
    }
}
