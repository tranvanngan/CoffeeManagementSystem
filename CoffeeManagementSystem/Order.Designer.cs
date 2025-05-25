namespace CoffeeManagementSystem
{
    partial class OrderForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDS = new System.Windows.Forms.Label();
            this.lblTimmon = new System.Windows.Forms.Label();
            this.txtTimkiemdouong = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDouong = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnTaoHoaDon = new System.Windows.Forms.Button();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDouong)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDS
            // 
            this.lblDS.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDS.AutoSize = true;
            this.lblDS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDS.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDS.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblDS.Location = new System.Drawing.Point(4, 39);
            this.lblDS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDS.Name = "lblDS";
            this.lblDS.Size = new System.Drawing.Size(351, 36);
            this.lblDS.TabIndex = 18;
            this.lblDS.Text = "DANH SÁCH ĐỒ UỐNG";
            // 
            // lblTimmon
            // 
            this.lblTimmon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTimmon.AutoSize = true;
            this.lblTimmon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTimmon.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimmon.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblTimmon.Location = new System.Drawing.Point(4, 94);
            this.lblTimmon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimmon.Name = "lblTimmon";
            this.lblTimmon.Size = new System.Drawing.Size(90, 21);
            this.lblTimmon.TabIndex = 20;
            this.lblTimmon.Text = "Tìm món:";
            this.lblTimmon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimkiemdouong
            // 
            this.txtTimkiemdouong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimkiemdouong.BackColor = System.Drawing.Color.White;
            this.txtTimkiemdouong.BorderColor = System.Drawing.Color.Black;
            this.txtTimkiemdouong.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.txtTimkiemdouong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimkiemdouong.DefaultText = "";
            this.txtTimkiemdouong.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimkiemdouong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimkiemdouong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimkiemdouong.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimkiemdouong.FillColor = System.Drawing.Color.BurlyWood;
            this.txtTimkiemdouong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimkiemdouong.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimkiemdouong.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtTimkiemdouong.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimkiemdouong.Location = new System.Drawing.Point(105, 94);
            this.txtTimkiemdouong.Margin = new System.Windows.Forms.Padding(7);
            this.txtTimkiemdouong.Name = "txtTimkiemdouong";
            this.txtTimkiemdouong.PlaceholderForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtTimkiemdouong.PlaceholderText = "Nhập món ăn cần tìm...";
            this.txtTimkiemdouong.SelectedText = "";
            this.txtTimkiemdouong.Size = new System.Drawing.Size(489, 52);
            this.txtTimkiemdouong.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaShell;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1222, 911);
            this.panel1.TabIndex = 2;
            // 
            // dgvDouong
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvDouong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDouong.BackgroundColor = System.Drawing.Color.SeaShell;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDouong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDouong.ColumnHeadersHeight = 18;
            this.dgvDouong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDouong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDouong.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDouong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDouong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDouong.Location = new System.Drawing.Point(0, 175);
            this.dgvDouong.Name = "dgvDouong";
            this.dgvDouong.RowHeadersVisible = false;
            this.dgvDouong.RowHeadersWidth = 51;
            this.dgvDouong.RowTemplate.Height = 24;
            this.dgvDouong.Size = new System.Drawing.Size(1222, 561);
            this.dgvDouong.TabIndex = 0;
            this.dgvDouong.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDouong.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDouong.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDouong.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDouong.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDouong.ThemeStyle.BackColor = System.Drawing.Color.SeaShell;
            this.dgvDouong.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDouong.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDouong.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDouong.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDouong.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDouong.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDouong.ThemeStyle.HeaderStyle.Height = 18;
            this.dgvDouong.ThemeStyle.ReadOnly = false;
            this.dgvDouong.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDouong.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDouong.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDouong.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDouong.ThemeStyle.RowsStyle.Height = 24;
            this.dgvDouong.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDouong.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Madouong";
            this.Column1.HeaderText = "Mã đồ uống";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Tendouong";
            this.Column2.HeaderText = "Tên đồ uống";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Maloai";
            this.Column3.HeaderText = "Loại đồ uống";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "CurrentGia";
            this.Column4.HeaderText = "Giá bán";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Mota";
            this.Column5.HeaderText = "Mô tả";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MistyRose;
            this.panel2.Controls.Add(this.lblDS);
            this.panel2.Controls.Add(this.lblTimmon);
            this.panel2.Controls.Add(this.txtTimkiemdouong);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1222, 175);
            this.panel2.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MistyRose;
            this.panel3.Controls.Add(this.btnTaoHoaDon);
            this.panel3.Controls.Add(this.lblStatusMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 736);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1222, 175);
            this.panel3.TabIndex = 22;
            // 
            // btnTaoHoaDon
            // 
            this.btnTaoHoaDon.ForeColor = System.Drawing.Color.Red;
            this.btnTaoHoaDon.Location = new System.Drawing.Point(947, 30);
            this.btnTaoHoaDon.Name = "btnTaoHoaDon";
            this.btnTaoHoaDon.Size = new System.Drawing.Size(146, 44);
            this.btnTaoHoaDon.TabIndex = 17;
            this.btnTaoHoaDon.Text = "Tạo hóa đơn";
            this.btnTaoHoaDon.UseVisualStyleBackColor = true;
            this.btnTaoHoaDon.Click += new System.EventHandler(this.btnTaoHoaDon_Click);
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMessage.ForeColor = System.Drawing.Color.Red;
            this.lblStatusMessage.Location = new System.Drawing.Point(40, 30);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(16, 20);
            this.lblStatusMessage.TabIndex = 16;
            this.lblStatusMessage.Text = "*";
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1222, 911);
            this.Controls.Add(this.dgvDouong);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OrderForm";
            this.Text = "Order";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDouong)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblDS;
        private System.Windows.Forms.Label lblTimmon;
        private Guna.UI2.WinForms.Guna2TextBox txtTimkiemdouong;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDouong;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.Button btnTaoHoaDon;
    }
}