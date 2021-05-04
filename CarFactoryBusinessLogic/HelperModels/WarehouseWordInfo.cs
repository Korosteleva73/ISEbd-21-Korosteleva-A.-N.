using System.Collections.Generic;
using CarFactoryBusinessLogic.ViewModels;

namespace CarFactoryBusinessLogic.HelperModels
{
    class WarehouseWordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<WarehouseViewModel> Warehouses { get; set; }
    }
}
