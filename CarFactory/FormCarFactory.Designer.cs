namespace CarFactoryView
{
    partial class FormCarFactory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer details = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (details != null))
            {
                details.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonCreateOrder = new System.Windows.Forms.Button();
            this.ButtonPayOrder = new System.Windows.Forms.Button();
            this.ButtonRef = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ДеталиtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.МашиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.исполнителиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.складыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.исполнителиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокМашинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.деталиМашиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЗаказовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокСкладовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.содержимоеСкладовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказыПоДатамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отдатьНаИзготовлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьСкладToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbcmvfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewCarFactory = new System.Windows.Forms.DataGridView();
            this.письмаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCarFactory)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonCreateOrder
            // 
            this.ButtonCreateOrder.Location = new System.Drawing.Point(948, 31);
            this.ButtonCreateOrder.Name = "ButtonCreateOrder";
            this.ButtonCreateOrder.Size = new System.Drawing.Size(192, 39);
            this.ButtonCreateOrder.TabIndex = 0;
            this.ButtonCreateOrder.Text = "Создать заказ";
            this.ButtonCreateOrder.UseVisualStyleBackColor = true;
            this.ButtonCreateOrder.Click += new System.EventHandler(this.ButtonCreateOrder_Click);
            // 
            // ButtonPayOrder
            // 
            this.ButtonPayOrder.Location = new System.Drawing.Point(948, 76);
            this.ButtonPayOrder.Name = "ButtonPayOrder";
            this.ButtonPayOrder.Size = new System.Drawing.Size(192, 36);
            this.ButtonPayOrder.TabIndex = 3;
            this.ButtonPayOrder.Text = "Заказ оплачен";
            this.ButtonPayOrder.UseVisualStyleBackColor = true;
            this.ButtonPayOrder.Click += new System.EventHandler(this.ButtonPayOrder_Click);
            // 
            // ButtonRef
            // 
            this.ButtonRef.Location = new System.Drawing.Point(948, 118);
            this.ButtonRef.Name = "ButtonRef";
            this.ButtonRef.Size = new System.Drawing.Size(192, 37);
            this.ButtonRef.TabIndex = 4;
            this.ButtonRef.Text = "Обновить список";
            this.ButtonRef.UseVisualStyleBackColor = true;
            this.ButtonRef.Click += new System.EventHandler(this.ButtonRef_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem,
            this.отчётыToolStripMenuItem,
            this.отдатьНаИзготовлениеToolStripMenuItem,
            this.fbcmvfToolStripMenuItem,
            this.письмаToolStripMenuItem});
            this.отдатьНаИзготовлениеToolStripMenuItem,
            this.пополнитьСкладToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1152, 28);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ДеталиtoolStripMenuItem,
            this.МашиныToolStripMenuItem,
            this.исполнителиToolStripMenuItem,
            this.складыToolStripMenuItem,
            this.клиентыToolStripMenuItem});
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.toolStripMenuItem.Text = "Справочники";
            // 
            // ДеталиtoolStripMenuItem
            // 
            this.ДеталиtoolStripMenuItem.Name = "ДеталиtoolStripMenuItem";
            this.ДеталиtoolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.ДеталиtoolStripMenuItem.Text = "Детали";
            this.ДеталиtoolStripMenuItem.Click += new System.EventHandler(this.ДеталиToolStripMenuItem_Click);
            // 
            // МашиныToolStripMenuItem
            // 
            this.МашиныToolStripMenuItem.Name = "МашиныToolStripMenuItem";
            this.МашиныToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.МашиныToolStripMenuItem.Text = "Машины";
            this.МашиныToolStripMenuItem.Click += new System.EventHandler(this.МашиныToolStripMenuItem_Click);
            // 
            // исполнителиToolStripMenuItem
            // 
            this.исполнителиToolStripMenuItem.Name = "исполнителиToolStripMenuItem";
            this.исполнителиToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.исполнителиToolStripMenuItem.Text = "Исполнители";
            this.исполнителиToolStripMenuItem.Click += new System.EventHandler(this.исполнителиToolStripMenuItem_Click);
            // 
            // складыToolStripMenuItem
            // 
            this.складыToolStripMenuItem.Name = "складыToolStripMenuItem";
            this.складыToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.складыToolStripMenuItem.Text = "Склады";
            this.складыToolStripMenuItem.Click += new System.EventHandler(this.СкладыToolStripMenuItem_Click);
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // исполнителиToolStripMenuItem
            // 
            this.исполнителиToolStripMenuItem.Name = "исполнителиToolStripMenuItem";
            this.исполнителиToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.исполнителиToolStripMenuItem.Text = "Исполнители";
            this.исполнителиToolStripMenuItem.Click += new System.EventHandler(this.исполнителиToolStripMenuItem_Click);
            // 
            // отчётыToolStripMenuItem
            // 
            this.отчётыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокМашинToolStripMenuItem,
            this.деталиМашиныToolStripMenuItem,
            this.списокЗаказовToolStripMenuItem,
            this.списокСкладовToolStripMenuItem,
            this.содержимоеСкладовToolStripMenuItem,
            this.заказыПоДатамToolStripMenuItem});
            this.отчётыToolStripMenuItem.Name = "отчётыToolStripMenuItem";
            this.отчётыToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.отчётыToolStripMenuItem.Text = "Отчёты";
            // 
            // списокМашинToolStripMenuItem
            // 
            this.списокМашинToolStripMenuItem.Name = "списокМашинToolStripMenuItem";
            this.списокМашинToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.списокМашинToolStripMenuItem.Text = "Список машин";
            this.списокМашинToolStripMenuItem.Click += new System.EventHandler(this.СписокМашинToolStripMenuItem_Click);
            // 
            // деталиМашиныToolStripMenuItem
            // 
            this.деталиМашиныToolStripMenuItem.Name = "деталиМашиныToolStripMenuItem";
            this.деталиМашиныToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.деталиМашиныToolStripMenuItem.Text = "Детали машины";
            this.деталиМашиныToolStripMenuItem.Click += new System.EventHandler(this.ДеталиМашиныToolStripMenuItem_Click);
            // 
            // списокЗаказовToolStripMenuItem
            // 
            this.списокЗаказовToolStripMenuItem.Name = "списокЗаказовToolStripMenuItem";
            this.списокЗаказовToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.списокЗаказовToolStripMenuItem.Text = "Список заказов";
            this.списокЗаказовToolStripMenuItem.Click += new System.EventHandler(this.СписокЗаказовToolStripMenuItem_Click);
            // 
            // списокСкладовToolStripMenuItem
            // 
            this.списокСкладовToolStripMenuItem.Name = "списокСкладовToolStripMenuItem";
            this.списокСкладовToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.списокСкладовToolStripMenuItem.Text = "Список складов";
            this.списокСкладовToolStripMenuItem.Click += new System.EventHandler(this.списокСкладовToolStripMenuItem_Click);
            // 
            // содержимоеСкладовToolStripMenuItem
            // 
            this.содержимоеСкладовToolStripMenuItem.Name = "содержимоеСкладовToolStripMenuItem";
            this.содержимоеСкладовToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.содержимоеСкладовToolStripMenuItem.Text = "Содержимое складов";
            this.содержимоеСкладовToolStripMenuItem.Click += new System.EventHandler(this.содержимоеСкладовToolStripMenuItem_Click);
            // 
            // заказыПоДатамToolStripMenuItem
            // 
            this.заказыПоДатамToolStripMenuItem.Name = "заказыПоДатамToolStripMenuItem";
            this.заказыПоДатамToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.заказыПоДатамToolStripMenuItem.Text = "Заказы по датам";
            this.заказыПоДатамToolStripMenuItem.Click += new System.EventHandler(this.заказыПоДатамToolStripMenuItem_Click);
            // 
            // отдатьНаИзготовлениеToolStripMenuItem
            // 
            this.отдатьНаИзготовлениеToolStripMenuItem.Name = "отдатьНаИзготовлениеToolStripMenuItem";
            this.отдатьНаИзготовлениеToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.отдатьНаИзготовлениеToolStripMenuItem.Text = "Отдать на исполнение";
            this.отдатьНаИзготовлениеToolStripMenuItem.Click += new System.EventHandler(this.отдатьНаИзготовлениеToolStripMenuItem_Click);
            // 
            // fbcmvfToolStripMenuItem
            // 
            this.fbcmvfToolStripMenuItem.Name = "fbcmvfToolStripMenuItem";
            this.fbcmvfToolStripMenuItem.Size = new System.Drawing.Size(14, 24);
            // 
            // пополнитьСкладToolStripMenuItem
            // 
            this.пополнитьСкладToolStripMenuItem.Name = "пополнитьСкладToolStripMenuItem";
            this.пополнитьСкладToolStripMenuItem.Size = new System.Drawing.Size(143, 24);
            this.пополнитьСкладToolStripMenuItem.Text = "Пополнить склад";
            this.пополнитьСкладToolStripMenuItem.Click += new System.EventHandler(this.ПополнитьСкладToolStripMenuItem_Click);
            // 
            // dataGridViewCarFactory
            // 
            this.dataGridViewCarFactory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCarFactory.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewCarFactory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCarFactory.Location = new System.Drawing.Point(13, 31);
            this.dataGridViewCarFactory.Name = "dataGridViewCarFactory";
            this.dataGridViewCarFactory.RowHeadersWidth = 51;
            this.dataGridViewCarFactory.RowTemplate.Height = 24;
            this.dataGridViewCarFactory.Size = new System.Drawing.Size(929, 408);
            this.dataGridViewCarFactory.TabIndex = 6;
            // 
            // письмаToolStripMenuItem
            // 
            this.письмаToolStripMenuItem.Name = "письмаToolStripMenuItem";
            this.письмаToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.письмаToolStripMenuItem.Text = "Письма";
            this.письмаToolStripMenuItem.Click += new System.EventHandler(this.письмаToolStripMenuItem_Click);
            // 
            // FormCarFactory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 450);
            this.Controls.Add(this.dataGridViewCarFactory);
            this.Controls.Add(this.ButtonRef);
            this.Controls.Add(this.ButtonPayOrder);
            this.Controls.Add(this.ButtonCreateOrder);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormCarFactory";
            this.Text = "Автомобильный завод";
            this.Load += new System.EventHandler(this.FormCarFactory_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCarFactory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonCreateOrder;
        private System.Windows.Forms.Button ButtonPayOrder;
        private System.Windows.Forms.Button ButtonRef;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewCarFactory;
        private System.Windows.Forms.ToolStripMenuItem ДеталиtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem МашиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокМашинToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem деталиМашиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокЗаказовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem складыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьСкладToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокСкладовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem содержимоеСкладовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказыПоДатамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отдатьНаИзготовлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem исполнителиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fbcmvfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem письмаToolStripMenuItem;
    }
}