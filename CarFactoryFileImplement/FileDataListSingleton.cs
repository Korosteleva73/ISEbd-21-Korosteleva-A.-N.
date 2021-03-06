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
        private readonly string DetailFileName = "Detail.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string CarFileName = "Car.xml";
        public List<Detail> Details { get; set; }
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        private FileDataListSingleton()
        {
            Details = LoadDetails();
            Orders = LoadOrders();
            Cars = LoadCars();
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
                new XElement("Count", order.Count),
                new XElement("Sum", order.Sum),
                new XElement("Status", (int)order.Status),
                new XElement("DateCreate", order.DateCreate),
                new XElement("DateImplement", order.DateImplement)));
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
    }
}
