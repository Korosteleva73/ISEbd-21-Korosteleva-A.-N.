using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
namespace CarFactoryView
{
    public partial class FormCar : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly CarLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> carDetails;
        public FormCar(CarLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }
        private void FormCar_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CarViewModel view = logic.Read(new CarBindingModel
                    {
                        Id =
    id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.CarName;
                        textBoxPrice.Text = view.Price.ToString();
                        carDetails = view.CarDetails;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                carDetails = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (carDetails != null)
                {
                    dataGridViewDetails.Rows.Clear();
                    foreach (var pc in carDetails)
                    {
                        dataGridViewDetails.Rows.Add(new object[] { pc.Key, pc.Value.Item1,
pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCarDetail>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (carDetails.ContainsKey(form.Id))
                {
                    carDetails[form.Id] = (form.DetailName, form.Count);
                }
                else
                {
                    carDetails.Add(form.Id, (form.DetailName, form.Count));
                }
                LoadData();
            }
        }
        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewDetails.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormCarDetail>();
                int id = Convert.ToInt32(dataGridViewDetails.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = carDetails[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    carDetails[form.Id] = (form.DetailName, form.Count);
                    LoadData();
                }
            }
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewDetails.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        carDetails.Remove(Convert.ToInt32(dataGridViewDetails.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (carDetails == null || carDetails.Count == 0)
            {
                MessageBox.Show("Заполните детали", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new CarBindingModel
                {
                    Id = id,
                    CarName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    CarDetails = carDetails
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
