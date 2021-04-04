using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
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
        public FormCarFactory(OrderLogic orderLogic)
        {
            InitializeComponent();
            this._orderLogic = orderLogic;
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
    }
}
