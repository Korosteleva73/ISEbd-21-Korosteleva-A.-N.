using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.Interfaces;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
namespace CarFactoryBusinessLogic.BusinessLogics
{
    public class DetailLogic
    {
        private readonly IDetailStorage _detailStorage;
        public DetailLogic(IDetailStorage detailStorage)
        {
            _detailStorage = detailStorage;
        }
        public List<DetailViewModel> Read(DetailBindingModel model)
        {
            if (model == null)
            {
                return _detailStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DetailViewModel> { _detailStorage.GetElement(model)
};
            }
            return _detailStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DetailBindingModel model)
        {
            var element = _detailStorage.GetElement(new DetailBindingModel
            {
                DetailName = model.DetailName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть деталь с таким названием");
            }
            if (model.Id.HasValue)
            {
                _detailStorage.Update(model);
            }
            else
            {
                _detailStorage.Insert(model);
            }
        }
        public void Delete(DetailBindingModel model)
        {
            var element = _detailStorage.GetElement(new DetailBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _detailStorage.Delete(model);
        }
    }
}
