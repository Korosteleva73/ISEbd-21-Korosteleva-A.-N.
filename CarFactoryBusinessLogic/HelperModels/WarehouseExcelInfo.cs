using System.Collections.Generic;
using CarFactoryBusinessLogic.ViewModels;

namespace CarFactoryBusinessLogic.HelperModels
{
    class WarehouseExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportWarehouseDetailsViewModel> WarehouseDetails { get; set; }
    }
}
