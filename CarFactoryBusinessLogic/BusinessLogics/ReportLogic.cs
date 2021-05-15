using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.HelperModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryBusinessLogic.BusinessLogics;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CarFactoryBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly ICarStorage _carStorage;
        private readonly IOrderStorage _orderStorage;

        public ReportLogic(ICarStorage carStorage, IWarehouseStorage
      warehouseStorage, IOrderStorage orderStorage)
        {
            _carStorage = carStorage;
            _warehouseStorage = warehouseStorage;
            _orderStorage = orderStorage;
        }

        public List<ReportCarDetailViewModel> GetCarDetails()
        {
            var cars = _carStorage.GetFullList();
            var list = new List<ReportCarDetailViewModel>();
            foreach (var car in cars)
            {
                var record = new ReportCarDetailViewModel
                {
                    CarName = car.CarName,
                    Details = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var detail in car.CarDetails)
                {
                    record.Details.Add(new Tuple<string, int>(detail.Value.Item1, detail.Value.Item2));
                    record.TotalCount += detail.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                CarName = x.CarName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }

        /// Сохранение детали в файл-Word
        public void SaveCarsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список машин",
                Cars = _carStorage.GetFullList()
            });
        }
        /// Сохранение детали с указанием продуктов в файл-Excel
        public void SaveCarDetailToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список деталей",
                CarDetails = GetCarDetails()
            });
        }
        /// Сохранение заказов в файл-Pdf
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        public List<ReportWarehouseDetailsViewModel> GetWarehouseDetails()
        {
            var warehouses = _warehouseStorage.GetFullList();
            var list = new List<ReportWarehouseDetailsViewModel>();
            foreach (var warehouse in warehouses)
            {
                var record = new ReportWarehouseDetailsViewModel
                {
                    WarehouseName = warehouse.WarehouseName,
                    Details = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var detail in warehouse.WarehouseDetails)
                {
                    record.Details.Add(new Tuple<string, int>(detail.Value.Item1, detail.Value.Item2));
                    record.TotalCount += detail.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<OrderReportByDateViewModel> GetOrderReportByDate(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
                .GroupBy(order => order.DateCreate.ToShortDateString())
                .Select(rec => new OrderReportByDateViewModel
                {
                    Date = Convert.ToDateTime(rec.Key),
                    Count = rec.Count(),
                    Sum = rec.Sum(order => order.Sum)
                })
                .ToList();
        }

        public void SaveWarehouseesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateWarehouseDoc(new WarehouseWordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Warehouses = _warehouseStorage.GetFullList()
            });
        }

        public void SaveWarehouseDetailsToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateWarehouseDoc(new WarehouseExcelInfo
            {
                FileName = model.FileName,
                Title = "Список загруженности складов",
                WarehouseDetails = GetWarehouseDetails()
            });
        }

        public void SaveOrderReportByDateToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDocOrderReportByDate(new PdfInfoOrderReportByDate
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrderReportByDate(model)
            });
        }
    }
}