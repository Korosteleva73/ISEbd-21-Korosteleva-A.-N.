using System;
using System.Collections.Generic;
using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;


namespace CarFactoryBusinessLogic.BusinessLogics
{
    public class WarehouseLogic
    {
        private readonly IWarehouseStorage _warehouseStorage;

        private readonly IDetailStorage _detailStorage;

        public WarehouseLogic(IWarehouseStorage warehouseStorage, IDetailStorage detailStorage)
        {
            _warehouseStorage = warehouseStorage;
            _detailStorage = detailStorage;
        }

        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return _warehouseStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel>
                {
                    _warehouseStorage.GetElement(model)
                };
            }

            return _warehouseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                WarehouseName = model.WarehouseName
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }

            if (model.Id.HasValue)
            {
                _warehouseStorage.Update(model);
            }
            else
            {
                _warehouseStorage.Insert(model);
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _warehouseStorage.Delete(model);
        }

        public void Replenishment(ReplenishWarehouseBindingModel model)
        {
            var warehouse = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = model.WarehouseId
            });

            var material = _detailStorage.GetElement(new DetailBindingModel
            {
                Id = model.DetailId
            });

            if (warehouse == null)
            {
                throw new Exception("Не найден склад");
            }

            if (material == null)
            {
                throw new Exception("Не найдена деталь");
            }

            if (warehouse.WarehouseDetails.ContainsKey(model.DetailId))
            {
                warehouse.WarehouseDetails[model.DetailId] = (material.DetailName, warehouse.WarehouseDetails[model.DetailId].Item2 + model.Count);
            }
            else
            {
                warehouse.WarehouseDetails.Add(material.Id, (material.DetailName, model.Count));
            }

            _warehouseStorage.Update(new WarehouseBindingModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                WarehouseBoss = warehouse.WarehouseBoss,
                DateCreate = warehouse.DateCreate,
                WarehouseDetails = warehouse.WarehouseDetails
            });
        }
    }
}
