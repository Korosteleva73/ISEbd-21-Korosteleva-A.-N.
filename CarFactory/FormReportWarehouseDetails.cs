﻿using System;
using System.Windows.Forms;
using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using Unity;

namespace CarFactoryView
{
    public partial class FormReportWarehouseDetails : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;
        public FormReportWarehouseDetails(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormReportWarehouseDetails_Load(object sender, EventArgs e)
        {
            try
            {
                var details = logic.GetWarehouseDetails();
                if (details != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var detail in details)
                    {
                        dataGridView.Rows.Add(new object[] { detail.WarehouseName, "", "" });
                        foreach (var listElem in detail.Details)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", detail.TotalCount });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveWarehouseDetailsToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
