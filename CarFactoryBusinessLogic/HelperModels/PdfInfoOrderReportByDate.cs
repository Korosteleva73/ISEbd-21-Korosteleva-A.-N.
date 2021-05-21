using System.Collections.Generic;
using CarFactoryBusinessLogic.ViewModels;

namespace CarFactoryBusinessLogic.HelperModels
{
    public class PdfInfoOrderReportByDate
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<OrderReportByDateViewModel> Orders { get; set; }
    }
}
