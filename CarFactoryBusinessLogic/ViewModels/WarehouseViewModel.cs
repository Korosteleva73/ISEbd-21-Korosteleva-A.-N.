using CarFactoryBusinessLogic.Attributes;
using CarFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class WarehouseViewModel
    {
        [Column(title: "Номер", width: 50)]
        [DataMember]
        public int Id { get; set; }

        [Column(title: "Название склада", width: 150)]
        [DataMember]
        public string WarehouseName { get; set; }

        [Column(title: "Ответсьвенный", width: 150)]
        [DataMember]
        public string WarehouseBoss { get; set; }

        [Column(title: "Дата создания", width: 100, format: "D")]
        [DataMember]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> WarehouseDetails { get; set; }
    }
}
