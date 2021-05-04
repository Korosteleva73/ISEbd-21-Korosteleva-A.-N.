using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;
using CarFactory;

namespace CarFactoryView
{
    public partial class FormCarFactory : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly OrderLogic _orderLogic;
        private readonly ReportLogic _reportLogic;
        public FormCarFactory(OrderLogic orderLogic,ReportLogic reportLogic)
        {
            InitializeComponent();
            this._orderLogic = orderLogic;
            this._reportLogic = reportLogic;
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
                    dataGridViewCarFactory.Columns[2].Visible = false;
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

        private void СписокМашинToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _reportLogic.SaveCarsToWordFile(new ReportBindingModel
                    {
                        FileName =
                   dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }
            }
        }


        private void ДеталиМашиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportCarDetails>();
            form.ShowDialog();
        }

        private void СписокЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportAboutOrders>();
            form.ShowDialog();
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

        private void списокСкладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _reportLogic.SaveWarehouseesToWordFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });

                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void содержимоеСкладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportWarehouseDetails>();
            form.ShowDialog();
        }

        private void заказыПоДатамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportOrdersByDate>();
            form.ShowDialog();
        }

    }
}
