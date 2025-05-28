﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagementSystem.BLL; 

namespace CoffeeManagementSystem
{
    public partial class EmployerForm : Form
    {
        private NhanvienBLL nhanvienBLL = new NhanvienBLL(); 
        public EmployerForm()
        {
            InitializeComponent();
            this.Load += EmployerForm_Load;
            dgvNhanvien.AutoGenerateColumns = false;
            dgvNhanvien.AllowUserToAddRows = false;
            dgvNhanvien.AllowUserToDeleteRows = false;
            dgvNhanvien.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Assign events to Employee tab controls
            this.txtSearch.KeyDown += new KeyEventHandler(txtSearch_KeyDown);
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            this.dgvNhanvien.CellClick += new DataGridViewCellEventHandler(dgvNhanvien_CellClick);
            this.btnThem.Click += new EventHandler(btnThem_Click);
        }
        private void EmployerForm_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanvien();
        }
        private void LoadDanhSachNhanvien()
        {
            try
            {
                // Gọi BLL để lấy danh sách nhân viên
                List<Nhanvien> danhSach = nhanvienBLL.GetAllNhanviens();
                dgvNhanvien.DataSource = danhSach;
                dgvNhanvien.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Thêm mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            FormChiTietNhanvien formChiTiet = new FormChiTietNhanvien();
            if (formChiTiet.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachNhanvien(); //Tải lại danh sách nhân viên
            }
        }
        //Tìm kiếm
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                string searchTerm = txtSearch.Text.Trim();
                LoadFilteredNhanvienData(searchTerm);
            }
        }
        //Hiển thị kết quả tìm kiếm
        private void LoadFilteredNhanvienData(string searchTerm)
        {
            try
            {
                List<Nhanvien> ketQuaHienThi;
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    // Gọi BLL để lấy tất cả nhân viên
                    ketQuaHienThi = nhanvienBLL.GetAllNhanviens(); 
                }
                else
                {
                    // Gọi BLL để tìm kiếm nhân viên
                    ketQuaHienThi = nhanvienBLL.SearchNhanviens(searchTerm); 
                }

                dgvNhanvien.DataSource = ketQuaHienThi;
                dgvNhanvien.Refresh();

                if (ketQuaHienThi.Count == 0 && !string.IsNullOrWhiteSpace(searchTerm))
                {
                    MessageBox.Show($"Không tìm thấy nhân viên nào phù hợp với từ khóa '{searchTerm}'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadtabPageNhanvien()
        {
            // ... logic tải UserControl FormNhanvien vào TabPage ...
        }
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Employer_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvNhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadFilteredNhanvienData(searchTerm);
        }


        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanvien.Rows.Count - (dgvNhanvien.AllowUserToAddRows ? 1 : 0))
            {
                Nhanvien selectedNhanvien = dgvNhanvien.Rows[e.RowIndex].DataBoundItem as Nhanvien;

                if (selectedNhanvien != null)
                {
                    try
                    {
                        FormChiTietNhanvien formChiTiet = new FormChiTietNhanvien(selectedNhanvien);

                        // Hiển thị Form Chi Tiết dưới dạng Dialog
                        if (formChiTiet.ShowDialog() == DialogResult.OK)
                        {
                            // Nếu Form Chi Tiết trả về DialogResult.OK (nghĩa là đã lưu thành công)
                            // Tải lại danh sách nhân viên trên Form chính
                            LoadDanhSachNhanvien();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lấy thông tin chi tiết nhân viên hoặc mở Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tabPageNhanvien_Click(object sender, EventArgs e)
        {

        }



        private void lblSearch_Click(object sender, EventArgs e)
        {

        }


    }
}