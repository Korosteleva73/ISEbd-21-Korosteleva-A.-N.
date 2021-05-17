using System;
using System.Collections.Generic;
using System.Linq;
using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFactoryDatabaseImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        public List<WarehouseViewModel> GetFullList()
        {
            using (var context = new CarFactoryDatabase())
            {
                return context.Warehouses
                    .Include(rec => rec.WarehouseDetails)
                    .ThenInclude(rec => rec.Detail)
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new CarFactoryDatabase())
            {
                return context.Warehouses
                    .Include(rec => rec.WarehouseDetails)
                    .ThenInclude(rec => rec.Detail)
                    .Where(rec => rec.WarehouseName
                    .Contains(model.WarehouseName))
                    .ToList()
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new CarFactoryDatabase())
            {
                var warehouse = context.Warehouses
                    .Include(rec => rec.WarehouseDetails)
                    .ThenInclude(rec => rec.Detail)
                    .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);

                return warehouse != null ?
                   CreateModel(warehouse):
                    null;
            }
        }

        public void Insert(WarehouseBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Warehouse(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(WarehouseBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);

                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }

                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Warehouses.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, CarFactoryDatabase context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.WarehouseBoss = model.WarehouseBoss;
            warehouse.DateCreate = model.DateCreate;

            if (warehouse.Id == 0)
            {
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var warehouseDetails = context.WarehouseDetails
                    .Where(rec => rec.WarehouseId == model.Id.Value)
                    .ToList();

                context.WarehouseDetails
                    .RemoveRange(warehouseDetails
                        .Where(rec => !model.WarehouseDetails
                            .ContainsKey(rec.DetailId))
                                .ToList());
                context.SaveChanges();

                foreach (var updateMaterial in warehouseDetails)
                {
                    updateMaterial.Count = model.WarehouseDetails[updateMaterial.DetailId].Item2;
                    model.WarehouseDetails.Remove(updateMaterial.DetailId);
                }
                context.SaveChanges();
            }

            foreach (var deteil in model.WarehouseDetails)
            {
                context.WarehouseDetails.Add(new WarehouseDetail
                {
                    WarehouseId = warehouse.Id,
                    DetailId = deteil.Key,
                    Count = deteil.Value.Item2
                });

                context.SaveChanges();
            }

            return warehouse;
        }

        public WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                WarehouseBoss = warehouse.WarehouseBoss,
                DateCreate = warehouse.DateCreate,
                WarehouseDetails = warehouse.WarehouseDetails
                            .ToDictionary(recPPC => recPPC.DetailId,
                            recPPC => (recPPC.Detail?.DetailName, recPPC.Count))
            }; 
        }
        public bool CheckDetails(CarViewModel model, int detailCountInOrder)
        {
            using (var context = new CarFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    foreach (var detaillsInCar in model.CarDetails)
                    {
                        int detailsCountInCar = detaillsInCar.Value.Item2 * detailCountInOrder;

                        List<WarehouseDetail> ownDetail = context.WarehouseDetails
                            .Where(storehouse => storehouse.DetailId == detaillsInCar.Key)
                            .ToList();

                        foreach (var detail in ownDetail)
                        {
                            int detailCountInWarehouse = detail.Count;

                            if (detailCountInWarehouse <= detailsCountInCar)
                            {
                                detailsCountInCar -= detailCountInWarehouse;
                                context.Warehouses.FirstOrDefault(rec => rec.Id == detail.WarehouseId).WarehouseDetails.Remove(detail);
                            }
                            else
                            {
                                detail.Count -= detailsCountInCar;
                                detailsCountInCar = 0;
                            }

                            if (detailsCountInCar == 0)
                            {
                                break;
                            }
                        }

                        if (detailsCountInCar > 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    context.SaveChanges();

                    transaction.Commit();

                    return true;
                }
            }
        }
    }
}
