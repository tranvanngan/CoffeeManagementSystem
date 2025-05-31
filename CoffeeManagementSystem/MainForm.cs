﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagementSystem
{
    public partial class MainForm : Form
    {
        // Thêm các biến để lưu trữ mã và tên nhân viên đăng nhập
        private string _loggedInMaNhanVien;
        private string _loggedInTenNhanVien;

        // Constructor mặc định (đã có sẵn)
        public MainForm()
        {
            InitializeComponent();
            LoadFormCon(new DashboardForm());
        }

        // Constructor MỚI để nhận thông tin nhân viên từ DangNhapForm
        public MainForm(string maNhanVien, string tenNhanVien) : this() 
        {
            _loggedInMaNhanVien = maNhanVien;
            _loggedInTenNhanVien = tenNhanVien;

            if (lblName != null) 
            {
                lblName.Text = tenNhanVien;
            }
        }

        private void LoadFormCon(Form formCon)
        {
            // Xóa form con hiện tại nếu có
            panelMain.Controls.Clear();

            // Cấu hình form con
            formCon.TopLevel = false;
            formCon.FormBorderStyle = FormBorderStyle.None;
            formCon.Dock = DockStyle.Fill;

            // Thêm vào panel và hiển thị
            panelMain.Controls.Add(formCon);
            formCon.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void add1_Load(object sender, EventArgs e)
        {
           
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            LoadFormCon(new DashboardForm());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            LoadFormCon(new CustomerForm());
        }

        private void btnEmployer_Click(object sender, EventArgs e)
        {          
            LoadFormCon(new EmployerForm());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTrangChu_Click_1(object sender, EventArgs e)
        {
            LoadFormCon(new DashboardForm());
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            LoadFormCon(new DrinkForm());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadFormCon(new ReportForm());
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
           
        }

        private void lblQuanLy_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadFormCon(new Infor(_loggedInMaNhanVien));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
           
            // 1. Ẩn MainForm hiện tại
            this.Hide();

            // 2. Tạo một thể hiện mới của DangNhapForm
            DangNhapForm loginForm = new DangNhapForm();

            // 3. Hiển thị DangNhapForm
            loginForm.Show();
            this.Close();         
        }

    }
}
