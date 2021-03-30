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
        private readonly IDetailStorage _detailStorage;
        private readonly ICarStorage _carStorage;
        private readonly IOrderStorage _orderStorage;
        public ReportLogic(ICarStorage carStorage, IDetailStorage
       detailStorage, IOrderStorage orderStorage)
        {
            _carStorage = carStorage;
            _detailStorage = detailStorage;
            _orderStorage = orderStorage;
        }
        /// Получение списка деталей с указанием, в каких изделиях используются
        public List<ReportCarDetailViewModel> GetCarDetail()
        {
            var details = _detailStorage.GetFullList();
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
                foreach (var detail in details)
                {
                    if (car.CarDetails.ContainsKey(detail.Id))
                    {
                        record.Details.Add(new Tuple<string, int>(detail.DetailName, car.CarDetails[detail.Id].Item2));
                        record.TotalCount += car.CarDetails[detail.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// Получение списка заказов за определенный период
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom =
           model.DateFrom,
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
                CarDetails = GetCarDetail()
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
    }
}