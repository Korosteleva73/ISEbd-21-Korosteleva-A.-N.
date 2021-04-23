using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryBusinessLogic.Enums;
using CarFactoryListImplement.Models;
using System;
using System.Collections.Generic;

namespace CarFactoryListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;

        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                result.Add(CreateModel(order));
            }
            return result;
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                if (order.CarId == model.CarId)
                {
                    if ((!model.DateFrom.HasValue && !model.DateTo.HasValue && order.DateCreate.Date == model.DateCreate.Date) ||
                         (model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate.Date >= model.DateFrom.Value.Date && order.DateCreate.Date <= model.DateTo.Value.Date) ||
                         (model.ClientId.HasValue && order.ClientId == model.ClientId) ||
                         (model.FreeOrders.HasValue && model.FreeOrders.Value && order.Status == OrderStatus.Принят) ||
                         (model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && order.Status == OrderStatus.Выполняется))
                    {
                        result.Add(CreateModel(order));
                    }
                }
            
            return result;
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || order.CarId ==
                model.CarId)
                {
                    return CreateModel(order);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order
            {
                Id = 1
            };
            foreach (var order in source.Orders)
            {
                if (order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }
        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.CarId = model.CarId;
            order.Count = model.Count;
            order.ClientId = model.ClientId.Value;
            order.ImplementerId = model.ImplementerId;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string carName = "";
            for (int i = 0; i < source.Cars.Count; ++i)
            {
                if (source.Cars[i].Id == order.CarId)
                {
                    carName = source.Cars[i].CarName;
                }
            }
            string clientFIO = "";
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == order.ClientId)
                {
                    clientFIO = source.Clients[i].ClientFIO;
                }
            }
            string implementerFIO = "";
            for (int i = 0; i < source.Implementers.Count; ++i)
                if (source.Implementers[i].Id == order.ImplementerId)
                {
                    clientFIO = source.Implementers[i].FIO;
                }

            return new OrderViewModel
            {
                Id = order.Id,
                CarId = order.CarId,
                CarName = carName,
                ClientId = order.ClientId,
                ClientFIO = clientFIO,
                ImplementerId = order.ImplementerId,
                ImplementerFIO = implementerFIO,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
            };
        }
    }
}
