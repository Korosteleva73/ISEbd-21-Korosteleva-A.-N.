using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;

namespace CarFactoryView
{
    public partial class FormWarehouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly WarehouseLogic logic;

        private int? id;

        private Dictionary<int, (string, int)> warehouseDetails;

        public FormWarehouse(WarehouseLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void LoadData()
        {
            try
            {
                if (warehouseDetails != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var warehouseDetail in warehouseDetails)
                    {
                        dataGridView.Rows.Add(new object[]
                        {
                            warehouseDetail.Key,
                            warehouseDetail.Value.Item1,
                            warehouseDetail.Value.Item2
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormWarehouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WarehouseViewModel view = logic.Read(new WarehouseBindingModel
                    {
                        Id = id.Value
                    })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.WarehouseName;
                        textBoxBoss.Text = view.WarehouseBoss;
                        warehouseDetails = view.WarehouseDetails;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                warehouseDetails = new Dictionary<int, (string, int)>();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBoxBoss.Text))
            {
                MessageBox.Show("Заполните сведения об ответственном", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                logic.CreateOrUpdate(new WarehouseBindingModel
                {
                    Id = id,
                    WarehouseName = textBoxName.Text,
                    WarehouseBoss = textBoxBoss.Text,
                    WarehouseDetails = warehouseDetails,
                    DateCreate = DateTime.Now
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
