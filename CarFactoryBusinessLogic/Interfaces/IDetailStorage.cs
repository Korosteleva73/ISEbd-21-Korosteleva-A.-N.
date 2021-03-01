using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.ViewModels;
using System.Collections.Generic;
namespace CarFactoryBusinessLogic.Interfaces
{
    public interface IDetailStorage
    {
        List<DetailViewModel> GetFullList();
        List<DetailViewModel> GetFilteredList(DetailBindingModel model);
        DetailViewModel GetElement(DetailBindingModel model);
        void Insert(DetailBindingModel model);
        void Update(DetailBindingModel model);
        void Delete(DetailBindingModel model);
    }
}
