using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace CarFactoryBusinessLogic.ViewModels
{
    [DataContract]
    public class ReportCarDetailViewModel
    {
        [DataMember]
        public string CarName { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public List<Tuple<string, int>> Details { get; set; }
    }
}
