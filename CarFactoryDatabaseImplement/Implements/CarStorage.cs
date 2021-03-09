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
    public class CarStorage : ICarStorage
    {
        public List<CarViewModel> GetFullList()
        {
            using (var context = new CarFactoryDatabase())
            {
                return context.Cars
                    .Include(rec => rec.CarDetails)
                    .ThenInclude(rec => rec.Detail)
                    .ToList()
                    .Select(rec => new CarViewModel
                    {
                        Id = rec.Id,
                        CarName = rec.CarName,
                        Price = rec.Price,
                        CarDetails = rec.CarDetails
                            .ToDictionary(recCarDetails => recCarDetails.DetailId,
                            recCarDetails => (recCarDetails.Detail?.DetailName,
                            recCarDetails.Count))
                    })
                    .ToList();
            }
        }
        public List<CarViewModel> GetFilteredList(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new CarFactoryDatabase())
            {
                return context.Cars
                    .Include(rec => rec.CarDetails)
                    .ThenInclude(rec => rec.Detail)
                    .Where(rec => rec.CarName.Contains(model.CarName))
                    .ToList()
                    .Select(rec => new CarViewModel
                    {
                        Id = rec.Id,
                        CarName = rec.CarName,
                        Price = rec.Price,
                        CarDetails = rec.CarDetails
                            .ToDictionary(recCarDetails => recCarDetails.DetailId,
                            recCarDetails => (recCarDetails.Detail?.DetailName,
                            recCarDetails.Count))
                    })
                    .ToList();
            }
        }
        public CarViewModel GetElement(CarBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new CarFactoryDatabase())
            {
                var car = context.Cars
                    .Include(rec => rec.CarDetails)
                    .ThenInclude(rec => rec.Detail)
                    .FirstOrDefault(rec => rec.CarName == model.CarName ||
                    rec.Id == model.Id);

                return car != null ?
                    new CarViewModel
                    {
                        Id = car.Id,
                        CarName = car.CarName,
                        Price = car.Price,
                        CarDetails = car.CarDetails
                            .ToDictionary(recCarDetail => recCarDetail.DetailId,
                            recCarDetail => (recCarDetail.Detail?.DetailName,
                            recCarDetail.Count))
                    } :
                    null;
            }
        }
        public void Insert(CarBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Car(), context);
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
        public void Update(CarBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var car = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);

                        if (car == null)
                        {
                            throw new Exception("Машина не найдена");
                        }

                        CreateModel(model, car, context);
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
        public void Delete(CarBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                var detail = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);

                if (detail == null)
                {
                    throw new Exception("Деталь не найдена");
                }

                context.Cars.Remove(detail);
                context.SaveChanges();
            }
        }
        private Car CreateModel(CarBindingModel model, Car car, CarFactoryDatabase context)
        {
            car.CarName = model.CarName;
            car.Price = model.Price;
            if (car.Id == 0)
            {
                context.Cars.Add(car);
                context.SaveChanges();
            }
            if (model.Id.HasValue)
            {
                var carDetail = context.CarDetails
                    .Where(rec => rec.CarId == model.Id.Value)
                    .ToList();

                context.CarDetails.RemoveRange(carDetail
                    .Where(rec => !model.CarDetails.ContainsKey(rec.CarId))
                    .ToList());
                context.SaveChanges();
                foreach (var updateDetail in carDetail)
                {
                    updateDetail.Count = model.CarDetails[updateDetail.DetailId].Item2;
                    model.CarDetails.Remove(updateDetail.CarId);
                }
                context.SaveChanges();
            }
            foreach (var carDetail in model.CarDetails)
            {
                context.CarDetails.Add(new CarDetail
                {
                    CarId = car.Id,
                    DetailId = carDetail.Key,
                    Count = carDetail.Value.Item2
                });
                context.SaveChanges();
            }
            return car;
        }
    }
}