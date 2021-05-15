using CarFactoryFileImplement.Models;
using CarFactoryBusinessLogic.Enums;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CarFactoryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string DetailFileName = "Detail.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string CarFileName = "Car.xml";
        private readonly string ClientFileName = "Client.xml";

        public List<Implementer> Implementers { get; set; }
        private readonly string WarehouseFileName = "Warehouse.xml";
        public List<Detail> Details { get; set; }
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        public List<Client> Clients { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        private FileDataListSingleton()
        {
            Details = LoadDetails();
            Orders = LoadOrders();
            Cars = LoadCars();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            Warehouses = LoadWarehouses();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveDetails();
            SaveOrders();
            SaveCars();
            SaveWarehouses();
            SaveClients();
            SaveImplementers();
        }
        private List<Detail> LoadDetails()
        {
            var list = new List<Detail>();
            if (File.Exists(DetailFileName))
            {
                XDocument xDocument = XDocument.Load(DetailFileName);
                var xElements = xDocument.Root.Elements("Detail").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Detail
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DetailName = elem.Element("DetailName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    Order order = new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        CarId = Convert.ToInt32(elem.Element("CarId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Convert.ToInt32(elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value)
                    };
                    if (!string.IsNullOrEmpty(elem.Element("DateImplement").Value))
                    {
                        order.DateImplement = Convert.ToDateTime(elem.Element("DateImplement").Value);
                    }
                    if (!string.IsNullOrEmpty(elem.Element("ImplementerId").Value))
                    {
                        order.ImplementerId = Convert.ToInt32(elem.Element("ImplementerId").Value);
                    }
                    list.Add(order);
                }
            }
            return list;
        }
        private List<Car> LoadCars()
        {
            var list = new List<Car>();
            if (File.Exists(CarFileName))
            {
                XDocument xDocument = XDocument.Load(CarFileName);
                var xElements = xDocument.Root.Elements("Car").ToList();
                foreach (var elem in xElements)
                {
                    var carDet = new Dictionary<int, int>();
                    foreach (var detail in
                   elem.Element("CarDetails").Elements("CarDetail").ToList())
                    {
                        carDet.Add(Convert.ToInt32(detail.Element("Key").Value),
                   Convert.ToInt32(detail.Element("Value").Value));
                    }
                    list.Add(new Car
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        CarName = elem.Element("CarName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        CarDetails = carDet
                    });
                }
            }
            return list;
        }
        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();

            if (File.Exists(WarehouseFileName))
            {
                XDocument xDocument = XDocument.Load(WarehouseFileName);

                var xElements = xDocument.Root.Elements("Warehouse").ToList();

                foreach (var warehouse in xElements)
                {
                    var warehouseDetails = new Dictionary<int, int>();

                    foreach (var detail in warehouse.Element("WarehouseMaterials").Elements("WarehouseMaterial").ToList())
                    {
                        warehouseDetails.Add(Convert.ToInt32(detail.Element("Key").Value), Convert.ToInt32(detail.Element("Value").Value));
                    }

                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(warehouse.Attribute("Id").Value),
                        WarehouseName = warehouse.Element("WarehouseName").Value,
                        WarehouseBoss = warehouse.Element("WarehouseBoss").Value,
                        DateCreate = Convert.ToDateTime(warehouse.Element("DateCreate").Value),
                        WarehouseDetails = warehouseDetails
                    });
                }
            }

            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Clients").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }
        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementers").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FIO = elem.Element("FIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value)
                    });
                }
            }
            return list;
        }
        private void SaveDetails()
        {
            if (Details != null)
            {
                var xElement = new XElement("Detail");
                foreach (var detail in Details)
                {
                    xElement.Add(new XElement("Detail",
                    new XAttribute("Id", detail.Id),
                    new XElement("DetailName", detail.DetailName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(DetailFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Order");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                     new XAttribute("Id", order.Id),
                     new XElement("CarId", order.CarId),
                     new XElement("ImplementerId", order.ImplementerId),
                     new XElement("Count", order.Count),
                     new XElement("Sum", order.Sum),
                     new XElement("Status", (int)order.Status),
                     new XElement("DateCreate", order.DateCreate),
                     new XElement("DateImplement", order.DateImplement.Value)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveCars()
        {
            if (Cars != null)
            {
                var xElement = new XElement("Cars");
                foreach (var car in Cars)
                {
                    var detElement = new XElement("CarDetails");
                    foreach (var detail in car.CarDetails)
                    {
                        detElement.Add(new XElement("CarDetail",
                        new XElement("Key", detail.Key),
                        new XElement("Value", detail.Value)));
                    }
                    xElement.Add(new XElement("Car",
                     new XAttribute("Id", car.Id),
                     new XElement("CarName", car.CarName),
                     new XElement("Price", car.Price),
                     detElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(CarFileName);
            }
        }
        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");

                foreach (var warehouse in Warehouses)
                {
                    var warehouseMaterials = new XElement("WarehouseMaterials");

                    foreach (var detail in warehouse.WarehouseDetails)
                    {
                        warehouseMaterials.Add(new XElement("WarehouseMaterial",
                            new XElement("Key", detail.Key),
                            new XElement("Value", detail.Value)));
                    }

                    xElement.Add(new XElement("Warehouse",
                        new XAttribute("Id", warehouse.Id),
                        new XElement("WarehouseName", warehouse.WarehouseName),
                        new XElement("WarehouseBoss", warehouse.WarehouseBoss),
                        new XElement("DateCreate", warehouse.DateCreate.ToString()),
                        warehouseMaterials));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("FIO", implementer.FIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
    }
}