using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CarFactoryBusinessLogic.ViewModels;
using CarFactoryBusinessLogic.BindingModels;
using CarFactoryWarehouseBossApp.Models;

namespace CarFactoryWarehouseBossApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIWarehouseBoss.GetRequest<List<WarehouseViewModel>>("api/warehouse/GetWarehouseList"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (password != Program.CurrentPassword)
                {
                    throw new Exception("Invalid password");
                }
                Program.Enter = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Enter Password");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(string warehouseName, string warehouseBoss)
        {
            if (!string.IsNullOrEmpty(warehouseName) && !string.IsNullOrEmpty(warehouseBoss))
            {
                APIWarehouseBoss.PostRequest("api/warehouse/CreateOrUpdateWarehouse", new WarehouseBindingModel
                {
                    WarehouseName = warehouseName,
                    WarehouseBoss = warehouseBoss,
                    DateCreate = DateTime.Now,
                    WarehouseDetails = new Dictionary<int, (string, int)>()
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Не введено ФИО ответственного или название склада");
        }

        [HttpGet]
        public IActionResult Update(int warehouseId)
        {
            var warehouse = APIWarehouseBoss.GetRequest<WarehouseViewModel>($"api/warehouse/GetWarehouse?warehouseId={warehouseId}");
            ViewBag.WarehouseDetails = warehouse.WarehouseDetails.Values;
            ViewBag.WarehouseName = warehouse.WarehouseName;
            ViewBag.WarehouseBoss = warehouse.WarehouseBoss;
            return View();
        }

        [HttpPost]
        public void Update(int warehouseId, string warehouseName, string warehouseBoss)
        {
            if (!string.IsNullOrEmpty(warehouseName) && !string.IsNullOrEmpty(warehouseBoss))
            {
                var warehouse = APIWarehouseBoss.GetRequest<WarehouseViewModel>($"api/warehouse/GetWarehouse?warehouseId={warehouseId}");
                if (warehouse == null)
                {
                    return;
                }
                APIWarehouseBoss.PostRequest("api/warehouse/CreateOrUpdateWarehouse", new WarehouseViewModel
                {
                    WarehouseName = warehouseName,
                    WarehouseBoss = warehouseBoss,
                    DateCreate = DateTime.Now,
                    WarehouseDetails = warehouse.WarehouseDetails,
                    Id = warehouse.Id
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введиде данные");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouse = APIWarehouseBoss.GetRequest<List<WarehouseViewModel>>("api/warehouse/GetWarehouseList");
            return View();
        }

        [HttpPost]
        public void Delete(int warehouseId)
        {
            APIWarehouseBoss.PostRequest("api/warehouse/DeleteWarehouse", new WarehouseBindingModel
            {
                Id = warehouseId
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Replenishment()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouse = APIWarehouseBoss.GetRequest<List<WarehouseViewModel>>("api/warehouse/GetWarehouseList");
            ViewBag.Details = APIWarehouseBoss.GetRequest<List<DetailViewModel>>("api/warehouse/GetDetailList");
            return View();
        }

        [HttpPost]
        public void Replenishment(int warehouseId, int detailId, int count)
        {
            APIWarehouseBoss.PostRequest("api/warehouse/ReplenishmentWarehouse", new ReplenishWarehouseBindingModel
            {
                WarehouseId = warehouseId,
                DetailId = detailId,
                Count = count
            });
            Response.Redirect("Replenishment");
        }
    }
}
