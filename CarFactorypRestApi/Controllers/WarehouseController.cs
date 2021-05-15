using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryBusinessLogic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : Controller
    {
        private readonly WarehouseLogic warehouseLogic;

        private readonly DetailLogic detailLogic;

        public WarehouseController(WarehouseLogic warehouseLogic, DetailLogic detailLogic)
        {
            this.warehouseLogic = warehouseLogic;
            this.detailLogic = detailLogic;
        }

        [HttpGet]
        public List<WarehouseViewModel> GetWarehouseList() => warehouseLogic.Read(null)?.ToList();

        [HttpPost]
        public void CreateOrUpdateWarehouse(WarehouseBindingModel model) => warehouseLogic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteWarehouse(WarehouseBindingModel model) => warehouseLogic.Delete(model);

        [HttpPost]
        public void ReplenishmentWarehouse(ReplenishWarehouseBindingModel model) => warehouseLogic.Replenishment(model);

        [HttpGet]
        public WarehouseViewModel GetWarehouse(int warehouseId) => warehouseLogic.Read(new WarehouseBindingModel { Id = warehouseId })?[0];

        [HttpGet]
        public List<DetailViewModel> GetDetailList() => detailLogic.Read(null);
    }
}