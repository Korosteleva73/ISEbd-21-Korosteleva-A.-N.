using System;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class OrderReportByDateViewModel
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
