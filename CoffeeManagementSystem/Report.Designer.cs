﻿namespace CoffeeManagementSystem
{
    partial class ReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBaoCao = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvProductSales = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPrintBestseller = new System.Windows.Forms.Button();
            this.lblTong = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpProductSalesEndDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpProductSalesStartDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvRevenue = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrintDoanhThu = new System.Windows.Forms.Button();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpRevenueEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpRevenueStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvPotentialCustomers = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrintKHtop = new System.Windows.Forms.Button();
            this.dtToTopSelling = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFromTopSelling = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlReports = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSales)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPotentialCustomers)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControlReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.lblBaoCao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1616, 27);
            this.panel1.TabIndex = 4;
            // 
            // lblBaoCao
            // 
            this.lblBaoCao.AutoSize = true;
            this.lblBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaoCao.Location = new System.Drawing.Point(3, 4);
            this.lblBaoCao.Name = "lblBaoCao";
            this.lblBaoCao.Size = new System.Drawing.Size(178, 20);
            this.lblBaoCao.TabIndex = 0;
            this.lblBaoCao.Text = "Báo Cáo Doanh Thu";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvProductSales);
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1608, 832);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "BestSeller";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvProductSales
            // 
            this.dgvProductSales.AllowUserToAddRows = false;
            this.dgvProductSales.AllowUserToDeleteRows = false;
            this.dgvProductSales.AllowUserToResizeColumns = false;
            this.dgvProductSales.AllowUserToResizeRows = false;
            this.dgvProductSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductSales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductSales.ColumnHeadersHeight = 35;
            this.dgvProductSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.Column5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn5});
            this.dgvProductSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductSales.EnableHeadersVisualStyles = false;
            this.dgvProductSales.Location = new System.Drawing.Point(0, 50);
            this.dgvProductSales.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvProductSales.Name = "dgvProductSales";
            this.dgvProductSales.RowHeadersVisible = false;
            this.dgvProductSales.RowHeadersWidth = 51;
            this.dgvProductSales.Size = new System.Drawing.Size(1608, 782);
            this.dgvProductSales.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.HeaderText = "STT";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 79;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Cost of Good Sold";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn6.HeaderText = "Giá";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 69;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.HeaderText = "Ngày giao dịch";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 168;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnPrintBestseller);
            this.panel4.Controls.Add(this.lblTong);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.dtpProductSalesEndDate);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.dtpProductSalesStartDate);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1608, 50);
            this.panel4.TabIndex = 2;
            // 
            // btnPrintBestseller
            // 
            this.btnPrintBestseller.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnPrintBestseller.FlatAppearance.BorderSize = 0;
            this.btnPrintBestseller.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintBestseller.Font = new System.Drawing.Font("Segoe UI Variable Small", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBestseller.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPrintBestseller.Image = global::CoffeeManagementSystem.Properties.Resources.Xuat;
            this.btnPrintBestseller.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintBestseller.Location = new System.Drawing.Point(1484, 5);
            this.btnPrintBestseller.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintBestseller.Name = "btnPrintBestseller";
            this.btnPrintBestseller.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.btnPrintBestseller.Size = new System.Drawing.Size(112, 41);
            this.btnPrintBestseller.TabIndex = 27;
            this.btnPrintBestseller.Text = "Xuất";
            this.btnPrintBestseller.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintBestseller.UseVisualStyleBackColor = false;
            this.btnPrintBestseller.Click += new System.EventHandler(this.btnPrintBestseller_Click);
            // 
            // lblTong
            // 
            this.lblTong.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.Location = new System.Drawing.Point(869, 14);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(120, 27);
            this.lblTong.TabIndex = 11;
            this.lblTong.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Purple;
            this.label7.Location = new System.Drawing.Point(644, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(202, 29);
            this.label7.TabIndex = 10;
            this.label7.Text = "Tổng doanh thu:";
            // 
            // dtpProductSalesEndDate
            // 
            this.dtpProductSalesEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpProductSalesEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpProductSalesEndDate.Location = new System.Drawing.Point(380, 12);
            this.dtpProductSalesEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpProductSalesEndDate.Name = "dtpProductSalesEndDate";
            this.dtpProductSalesEndDate.Size = new System.Drawing.Size(159, 30);
            this.dtpProductSalesEndDate.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(333, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "To :";
            // 
            // dtpProductSalesStartDate
            // 
            this.dtpProductSalesStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpProductSalesStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpProductSalesStartDate.Location = new System.Drawing.Point(159, 12);
            this.dtpProductSalesStartDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpProductSalesStartDate.Name = "dtpProductSalesStartDate";
            this.dtpProductSalesStartDate.Size = new System.Drawing.Size(157, 30);
            this.dtpProductSalesStartDate.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "Filter By : From";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvRevenue);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(1608, 832);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Doanh Thu";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvRevenue
            // 
            this.dgvRevenue.AllowUserToAddRows = false;
            this.dgvRevenue.AllowUserToDeleteRows = false;
            this.dgvRevenue.AllowUserToResizeColumns = false;
            this.dgvRevenue.AllowUserToResizeRows = false;
            this.dgvRevenue.BackgroundColor = System.Drawing.Color.White;
            this.dgvRevenue.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvRevenue.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRevenue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRevenue.ColumnHeadersHeight = 35;
            this.dgvRevenue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRevenue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4});
            this.dgvRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRevenue.EnableHeadersVisualStyles = false;
            this.dgvRevenue.Location = new System.Drawing.Point(3, 52);
            this.dgvRevenue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvRevenue.Name = "dgvRevenue";
            this.dgvRevenue.RowHeadersVisible = false;
            this.dgvRevenue.RowHeadersWidth = 51;
            this.dgvRevenue.Size = new System.Drawing.Size(1602, 778);
            this.dgvRevenue.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "STT";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 79;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Ngày giao dịch";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "Doanh thu";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 129;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnPrintDoanhThu);
            this.panel3.Controls.Add(this.lblTotalPrice);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.dtpRevenueEndDate);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.dtpRevenueStartDate);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1602, 50);
            this.panel3.TabIndex = 1;
            // 
            // btnPrintDoanhThu
            // 
            this.btnPrintDoanhThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnPrintDoanhThu.FlatAppearance.BorderSize = 0;
            this.btnPrintDoanhThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintDoanhThu.Font = new System.Drawing.Font("Segoe UI Variable Small", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintDoanhThu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPrintDoanhThu.Image = global::CoffeeManagementSystem.Properties.Resources.Xuat;
            this.btnPrintDoanhThu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintDoanhThu.Location = new System.Drawing.Point(1481, 5);
            this.btnPrintDoanhThu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintDoanhThu.Name = "btnPrintDoanhThu";
            this.btnPrintDoanhThu.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.btnPrintDoanhThu.Size = new System.Drawing.Size(112, 41);
            this.btnPrintDoanhThu.TabIndex = 26;
            this.btnPrintDoanhThu.Text = "Xuất";
            this.btnPrintDoanhThu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintDoanhThu.UseVisualStyleBackColor = false;
            this.btnPrintDoanhThu.Click += new System.EventHandler(this.btnPrintDoanhThu_Click);
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPrice.Location = new System.Drawing.Point(835, 14);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(120, 27);
            this.lblTotalPrice.TabIndex = 11;
            this.lblTotalPrice.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Purple;
            this.label5.Location = new System.Drawing.Point(609, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 29);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tổng doanh thu:";
            // 
            // dtpRevenueEndDate
            // 
            this.dtpRevenueEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpRevenueEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRevenueEndDate.Location = new System.Drawing.Point(372, 12);
            this.dtpRevenueEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpRevenueEndDate.Name = "dtpRevenueEndDate";
            this.dtpRevenueEndDate.Size = new System.Drawing.Size(157, 30);
            this.dtpRevenueEndDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "To :";
            // 
            // dtpRevenueStartDate
            // 
            this.dtpRevenueStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpRevenueStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRevenueStartDate.Location = new System.Drawing.Point(159, 12);
            this.dtpRevenueStartDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpRevenueStartDate.Name = "dtpRevenueStartDate";
            this.dtpRevenueStartDate.Size = new System.Drawing.Size(156, 30);
            this.dtpRevenueStartDate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Filter By : From";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvPotentialCustomers);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1608, 832);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Khách Hàng Tiềm Năng";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvPotentialCustomers
            // 
            this.dgvPotentialCustomers.AllowUserToOrderColumns = true;
            this.dgvPotentialCustomers.AllowUserToResizeColumns = false;
            this.dgvPotentialCustomers.AllowUserToResizeRows = false;
            this.dgvPotentialCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvPotentialCustomers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPotentialCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPotentialCustomers.ColumnHeadersHeight = 50;
            this.dgvPotentialCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPotentialCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column7,
            this.Column6});
            this.dgvPotentialCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPotentialCustomers.EnableHeadersVisualStyles = false;
            this.dgvPotentialCustomers.Location = new System.Drawing.Point(3, 52);
            this.dgvPotentialCustomers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPotentialCustomers.Name = "dgvPotentialCustomers";
            this.dgvPotentialCustomers.RowHeadersVisible = false;
            this.dgvPotentialCustomers.RowHeadersWidth = 51;
            this.dgvPotentialCustomers.Size = new System.Drawing.Size(1602, 778);
            this.dgvPotentialCustomers.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.DataPropertyName = "Makhachhang";
            this.Column1.HeaderText = "Mã KH";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "Hoten";
            this.Column2.HeaderText = "Họ Tên";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.DataPropertyName = "Sodienthoai";
            this.Column3.HeaderText = "Số Điện Thoại";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 164;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "Email";
            this.Column4.HeaderText = "Email";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 87;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Ngaydangky";
            this.Column7.HeaderText = "Ngày đăng ký";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.Width = 125;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Diemtichluy";
            this.Column6.HeaderText = "Điểm tích lũy";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.Width = 125;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPrintKHtop);
            this.panel2.Controls.Add(this.dtToTopSelling);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtFromTopSelling);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1602, 50);
            this.panel2.TabIndex = 0;
            // 
            // btnPrintKHtop
            // 
            this.btnPrintKHtop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnPrintKHtop.FlatAppearance.BorderSize = 0;
            this.btnPrintKHtop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintKHtop.Font = new System.Drawing.Font("Segoe UI Variable Small", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintKHtop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPrintKHtop.Image = global::CoffeeManagementSystem.Properties.Resources.Xuat;
            this.btnPrintKHtop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintKHtop.Location = new System.Drawing.Point(1481, 5);
            this.btnPrintKHtop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintKHtop.Name = "btnPrintKHtop";
            this.btnPrintKHtop.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.btnPrintKHtop.Size = new System.Drawing.Size(112, 41);
            this.btnPrintKHtop.TabIndex = 24;
            this.btnPrintKHtop.Text = "Xuất";
            this.btnPrintKHtop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintKHtop.UseVisualStyleBackColor = false;
            this.btnPrintKHtop.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtToTopSelling
            // 
            this.dtToTopSelling.CustomFormat = "dd/MM/yyyy";
            this.dtToTopSelling.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToTopSelling.Location = new System.Drawing.Point(391, 12);
            this.dtToTopSelling.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtToTopSelling.Name = "dtToTopSelling";
            this.dtToTopSelling.Size = new System.Drawing.Size(156, 30);
            this.dtToTopSelling.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "To :";
            // 
            // dtFromTopSelling
            // 
            this.dtFromTopSelling.CustomFormat = "dd/MM/yyyy";
            this.dtFromTopSelling.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromTopSelling.Location = new System.Drawing.Point(159, 12);
            this.dtFromTopSelling.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtFromTopSelling.Name = "dtFromTopSelling";
            this.dtFromTopSelling.Size = new System.Drawing.Size(157, 30);
            this.dtFromTopSelling.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By : From";
            // 
            // tabControlReports
            // 
            this.tabControlReports.Controls.Add(this.tabPage1);
            this.tabControlReports.Controls.Add(this.tabPage2);
            this.tabControlReports.Controls.Add(this.tabPage3);
            this.tabControlReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlReports.ItemSize = new System.Drawing.Size(180, 30);
            this.tabControlReports.Location = new System.Drawing.Point(0, 27);
            this.tabControlReports.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControlReports.Name = "tabControlReports";
            this.tabControlReports.SelectedIndex = 0;
            this.tabControlReports.Size = new System.Drawing.Size(1616, 870);
            this.tabControlReports.TabIndex = 5;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1616, 897);
            this.Controls.Add(this.tabControlReports);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ReportForm";
            this.Text = "Report";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSales)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPotentialCustomers)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControlReports.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBaoCao;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpProductSalesEndDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpProductSalesStartDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvRevenue;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpRevenueEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpRevenueStartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvPotentialCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtToTopSelling;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFromTopSelling;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlReports;
        private System.Windows.Forms.DataGridView dgvProductSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button btnPrintKHtop;
        private System.Windows.Forms.Button btnPrintBestseller;
        private System.Windows.Forms.Button btnPrintDoanhThu;
    }
}