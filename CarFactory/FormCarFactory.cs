using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
namespace CarFactoryView
{
    public partial class FormCarFactory : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly OrderLogic _orderLogic;
        private readonly CarLogic _carLogic;
        public FormCarFactory(OrderLogic orderLogic, CarLogic carLogic)
        {
            InitializeComponent();
            this._orderLogic = orderLogic;
            this._carLogic = carLogic;
        }
        private void FormCarFactory_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var ordersList = _orderLogic.Read(null);
                if (ordersList != null)
                {
                    var carsList = _carLogic.Read(null);
                    foreach (OrderViewModel order in ordersList)
                    {
                        foreach (CarViewModel car in carsList)
                        {
                            if (car.Id == order.CarId)
                            {
                                order.CarName = car.CarName;
                            }
                        }
                    }
                    dataGridViewCarFactory.DataSource = ordersList;
                    dataGridViewCarFactory.Columns[0].Visible = false;
                    dataGridViewCarFactory.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ДеталиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormDetails>();
            form.ShowDialog();
        }
        private void МашиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCars>();
            form.ShowDialog();
        }
        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }
        private void ButtonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridViewCarFactory.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewCarFactory.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                    {
                        OrderId =
                   id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridViewCarFactory.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewCarFactory.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.FinishOrder(new ChangeStatusBindingModel
                    {
                        OrderId = id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewCarFactory.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewCarFactory.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ПополнитьСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWarehouseReplenishment>();
            form.ShowDialog();
        }

        private void СкладыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWarehouses>();
            form.ShowDialog();
        }
    }
}
