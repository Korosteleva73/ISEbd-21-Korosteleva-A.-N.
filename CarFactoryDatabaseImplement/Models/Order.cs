﻿using System;
using CarFactoryBusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarFactoryDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Car Car { get; set; }
    }
}

