using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CarFactoryDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new CarFactoryDatabase())
            {
                return context.Orders
                    .Include(rec =>rec.Car)
                    .Select(rec => new OrderViewModel
                    {
                        Id = rec.Id,
                        CarName = rec.Car.CarName,
                        CarId = rec.CarId,
                        Count = rec.Count,
                        Sum = rec.Sum,
                        Status = rec.Status,
                        DateCreate = rec.DateCreate,
                        DateImplement = rec.DateImplement
                    })
                    .ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDatabase())
            {
                return context.Orders
.Include(rec => rec.Car)
.Include(rec => rec.Client)
.Where(rec => (!model.DateFrom.HasValue &&
!model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) ||
(model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >=
model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
(model.ClientId.HasValue && rec.ClientId == model.ClientId))
.Select(CreateModel).ToList();
            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDatabase())
            {
                var order = context.Orders.Include(rec => rec.Car)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return order != null ?
                    new OrderViewModel
                    {
                        Id = order.Id,
                        CarName = context.Cars.FirstOrDefault(rec => rec.Id == order.CarId)?.CarName,
                        CarId = order.CarId,
                        Count = order.Count,
                        Sum = order.Sum,
                        Status = order.Status,
                        DateCreate = order.DateCreate,
                        DateImplement = order.DateImplement
                    } :
                    null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }

                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);

                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }
                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }
        private OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                CarId = order.CarId,
                ClientId = order.ClientId,
                ClientFIO = order.Client.ClientFIO,
                CarName = order.Car.CarName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order?.DateImplement
            };
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.CarId = model.CarId;
            order.ClientId = Convert.ToInt32(model.ClientId);
            order.Count = model.Count;
            order.Status = model.Status;
            order.Sum = model.Sum;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
    }
}