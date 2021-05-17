using CarFactoryBusinessLogic.Attributes;
using System.Runtime.Serialization;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class DetailViewModel
    {
        [Column(title: "Номер", width: 50)]
        [DataMember]
        public int Id { get; set; }
        [Column(title: "Название", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string DetailName { get; set; }
    }
}
