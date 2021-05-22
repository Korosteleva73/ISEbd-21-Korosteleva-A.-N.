using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;

        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.WarehouseBoss = model.WarehouseBoss;

            foreach (var key in warehouse.WarehouseDetails.Keys.ToList())
            {
                if (!model.WarehouseDetails.ContainsKey(key))
                {
                    warehouse.WarehouseDetails.Remove(key);
                }
            }

            foreach (var detail in model.WarehouseDetails)
            {
                if (warehouse.WarehouseDetails.ContainsKey(detail.Key))
                {
                    warehouse.WarehouseDetails[detail.Key] =
                        model.WarehouseDetails[detail.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseDetails.Add(detail.Key, model.WarehouseDetails[detail.Key].Item2);
                }
            }

            return warehouse;
        }

        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            Dictionary<int, (string, int)> warehouseDetails = new Dictionary<int, (string, int)>();

            foreach (var warehouseDetail in warehouse.WarehouseDetails)
            {
                string detailName = string.Empty;
                foreach (var detail in source.Details)
                {
                    if (warehouseDetail.Key == detail.Id)
                    {
                        detailName = detail.DetailName;
                        break;
                    }
                }
                warehouseDetails.Add(warehouseDetail.Key, (detailName, warehouseDetail.Value));
            }

            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                WarehouseBoss = warehouse.WarehouseBoss,
                DateCreate = warehouse.DateCreate,
                WarehouseDetails = warehouseDetails
            };
        }

        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses.Select(CreateModel).ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.Warehouses
                .Where(warehouse => warehouse.WarehouseName
                .Contains(model.WarehouseName))
                .Select(CreateModel).ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var warehouse = source.Warehouses.
                FirstOrDefault(Xwarehouse => Xwarehouse.WarehouseName == model.WarehouseName || Xwarehouse.Id == model.Id);

            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Warehouses.Max(Xwarehouse => Xwarehouse.Id) : 0;
            var warehouse = new Warehouse { Id = maxId + 1, WarehouseDetails = new Dictionary<int, int>(), DateCreate = DateTime.Now };
            source.Warehouses.Add(CreateModel(model, warehouse));
        }

        public void Update(WarehouseBindingModel model)
        {
            var warehouse = source.Warehouses.FirstOrDefault(Xwarehouse => Xwarehouse.Id == model.Id);

            if (warehouse == null)
            {
                throw new Exception("Склад не найден");
            }

            CreateModel(model, warehouse);
        }

        public void Delete(WarehouseBindingModel model)
        {
            var warehouse = source.Warehouses.FirstOrDefault(Xwarehouse => Xwarehouse.Id == model.Id);

            if (warehouse != null)
            {
                source.Warehouses.Remove(warehouse);
            }
            else
            {
                throw new Exception("Склад не найден");
            }
        }

        public bool TakeDetails(Dictionary<int, (string, int)> details, int necessaryDetailsCount)
        {
            foreach (var detail in details)
            {
                int availablyDetailscount = source.Warehouses.
                    Where(car => car.WarehouseDetails
                    .ContainsKey(detail.Key))
                    .Sum(car => car.WarehouseDetails[detail.Key]);

                if (availablyDetailscount < detail.Value.Item2 * necessaryDetailsCount)
                {
                    return false;
                }
            }

            foreach (var detail in details)
            {
                int availablyDetailscount = detail.Value.Item2 * necessaryDetailsCount;
                IEnumerable<Warehouse> warehouses = source.Warehouses.Where(car => car.WarehouseDetails.ContainsKey(detail.Key));

                foreach (Warehouse storage in warehouses)
                {
                    if (storage.WarehouseDetails[detail.Key] <= availablyDetailscount)
                    {
                        availablyDetailscount -= storage.WarehouseDetails[detail.Key];
                        storage.WarehouseDetails.Remove(detail.Key);
                    }
                    else
                    {
                        storage.WarehouseDetails[detail.Key] -= availablyDetailscount;
                        availablyDetailscount = 0;
                    }

                    if (availablyDetailscount == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        public bool CheckDetails(CarViewModel model, int detailCountInOrder)
        {
            throw new NotImplementedException();
        }
    }
}
