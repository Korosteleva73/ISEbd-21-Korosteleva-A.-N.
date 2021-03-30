using CarFactoryBusinessLogic.ViewModels;
using System.Collections.Generic;
namespace CarFactoryBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportCarDetailViewModel> CarDetails { get; set; }
    }
}