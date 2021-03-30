﻿using System;
using System.Collections.Generic;
namespace CarFactoryBusinessLogic.ViewModels
{
    public class ReportCarDetailViewModel
    {
        public string CarName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Details { get; set; }
    }
}
