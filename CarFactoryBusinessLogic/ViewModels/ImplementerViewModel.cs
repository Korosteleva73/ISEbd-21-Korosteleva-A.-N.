using System.ComponentModel;

namespace CarFactoryBusinessLogic.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО исполнителя")]
        public string FIO { get; set; }
        [DisplayName("Время на заказ")]
        public int WorkingTime { get; set; }
        [DisplayName("Время на перерыв")]
        public int PauseTime { get; set; }
    }
}
