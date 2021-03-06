using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;

namespace CarFactoryView
{
    public partial class FormWarehouseReplenishment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int DetailId
        {
            get
            {
                return Convert.ToInt32(comboBoxDetail.SelectedValue);
            }
            set
            {
                comboBoxDetail.SelectedValue = value;
            }
        }

        public int Warehouse
        {
            get
            {
                return Convert.ToInt32(comboBoxName.SelectedValue);
            }
            set
            {
                comboBoxName.SelectedValue = value;
            }
        }

        public int Count
        {
            get
            {
                return Convert.ToInt32(textBoxCount.Text);
            }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        private readonly WarehouseLogic _warehouseLogic;

        public FormWarehouseReplenishment(DetailLogic detailLogic, WarehouseLogic warehouseLogic)
        {
            InitializeComponent();

            _warehouseLogic = warehouseLogic;

            List<DetailViewModel> listDetails = detailLogic.Read(null);

            if (listDetails != null)
            {
                comboBoxDetail.DisplayMember = "DetailName";
                comboBoxDetail.ValueMember = "Id";
                comboBoxDetail.DataSource = listDetails;
                comboBoxDetail.SelectedItem = null;
            }

            List<WarehouseViewModel> listWarehouse = warehouseLogic.Read(null);

            if (listWarehouse != null)
            {
                comboBoxName.DisplayMember = "WarehouseName";
                comboBoxName.ValueMember = "Id";
                comboBoxName.DataSource = listWarehouse;
                comboBoxName.SelectedItem = null;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxName.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxDetail.SelectedValue == null)
            {
                MessageBox.Show("Выберите деталь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Неизвестное количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _warehouseLogic.Replenishment(new ReplenishWarehouseBindingModel
            {
                WarehouseId = Convert.ToInt32(comboBoxName.SelectedValue),
                DetailId = Convert.ToInt32(comboBoxDetail.SelectedValue),
                Count = Convert.ToInt32(textBoxCount.Text)
            });

            DialogResult = DialogResult.OK;

            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

