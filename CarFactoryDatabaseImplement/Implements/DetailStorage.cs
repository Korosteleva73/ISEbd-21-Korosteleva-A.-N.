using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CarFactoryDatabaseImplement.Implements
{
    public class DetailStorage : IDetailStorage
    {
        public List<DetailViewModel> GetFullList()
        {
            using (var context = new CarFactoryDatabase())
            {
                return context.Details
                .Select(rec => new DetailViewModel
                {
                    Id = rec.Id,
                    DetailName = rec.DetailName
                })
               .ToList();
            }
        }
        public List<DetailViewModel> GetFilteredList(DetailBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDatabase())
            {
                return context.Details
                .Where(rec => rec.DetailName.Contains(model.DetailName))
               .Select(rec => new DetailViewModel
               {
                   Id = rec.Id,
                   DetailName = rec.DetailName
               })
                .ToList();
            }
        }
        public DetailViewModel GetElement(DetailBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new CarFactoryDatabase())
            {
                var detail = context.Details
                .FirstOrDefault(rec => rec.DetailName == model.DetailName ||
               rec.Id == model.Id);
                return detail != null ?
                new DetailViewModel
                {
                    Id = detail.Id,
                    DetailName = detail.DetailName
                } :
               null;
            }
        }
        public void Insert(DetailBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                context.Details.Add(CreateModel(model, new Detail()));
                context.SaveChanges();
            }
        }
        public void Update(DetailBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                var element = context.Details.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(DetailBindingModel model)
        {
            using (var context = new CarFactoryDatabase())
            {
                Detail element = context.Details.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Details.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Detail CreateModel(DetailBindingModel model, Detail detail)
        {
            detail.DetailName = model.DetailName;
            return detail;
        }
    }
}