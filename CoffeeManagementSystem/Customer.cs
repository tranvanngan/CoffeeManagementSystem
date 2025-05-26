﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagementSystem.BLL; // Đảm bảo đã using namespace chứa KhachhangBLL

namespace CoffeeManagementSystem
{
    public partial class CustomerForm : Form
    {
        private KhachhangBLL khachhangBLL; // Thay đổi từ KhachhangDAL sang KhachhangBLL

        // Constructor của Form
        public CustomerForm()
        {
            InitializeComponent();
            khachhangBLL = new KhachhangBLL(); // Khởi tạo đối tượng BLL
            LoadDanhSachKhachHang();
            // Gán sự kiện TextChanged cho TextBox tìm kiếm (để tìm khi gõ)
            this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            // Gán sự kiện CellClick cho DataGridView (để hiển thị Form Chi Tiết)
            this.dgvKhachHang.CellClick += new DataGridViewCellEventHandler(dgvKhachHang_CellClick);

            // Gán sự kiện Click cho nút Thêm mới (giả định tên nút là btnAdd)
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
        }

        // Sự kiện Form Load: Tải dữ liệu khi Form được hiển thị
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhachHang(); // Gọi phương thức tải danh sách ban đầu
        }

        // Phương thức tải danh sách khách hàng và hiển thị lên DataGridView
        private void LoadDanhSachKhachHang()
        {
            try
            {
                // Lấy danh sách khách hàng từ BLL
                List<Khachhang> danhSach = khachhangBLL.GetAllKhachhangs(); // Gọi BLL

                // Gán danh sách làm nguồn dữ liệu cho DataGridView
                dgvKhachHang.DataSource = danhSach;

                // Tùy chọn: Tự động điều chỉnh kích thước cột
                // dgvKhachHang.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (InvalidOperationException bllEx) // Bắt lỗi nghiệp vụ từ BLL
            {
                MessageBox.Show("Lỗi nghiệp vụ khi tải danh sách khách hàng: " + bllEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // Bắt các lỗi khác (ví dụ: lỗi kết nối CSDL)
            {
                // Xử lý lỗi nếu không tải được dữ liệu
                MessageBox.Show("Không thể tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFilteredData(string searchTerm)
        {
            try
            {
                List<Khachhang> ketQuaHienThi;

                // Kiểm tra nếu từ khóa tìm kiếm rỗng hoặc chỉ chứa khoảng trắng
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    // Nếu trống, tải toàn bộ danh sách khách hàng
                    ketQuaHienThi = khachhangBLL.GetAllKhachhangs(); // Gọi BLL
                }
                else
                {
                    // Nếu có từ khóa, gọi phương thức tìm kiếm
                    ketQuaHienThi = khachhangBLL.SearchKhachhangs(searchTerm); // Gọi BLL
                }

                // Gán kết quả (toàn bộ hoặc đã lọc) làm nguồn dữ liệu cho DataGridView
                dgvKhachHang.DataSource = ketQuaHienThi;

                // Tùy chọn: Thông báo nếu không tìm thấy kết quả (chỉ khi có từ khóa và kết quả rỗng)
                if (ketQuaHienThi.Count == 0 && !string.IsNullOrWhiteSpace(searchTerm))
                {
                    // Bạn có thể hiển thị thông báo hoặc không, tùy ý
                    // MessageBox.Show($"Không tìm thấy khách hàng nào phù hợp với từ khóa '{searchTerm}'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (InvalidOperationException bllEx) // Bắt lỗi nghiệp vụ từ BLL
            {
                MessageBox.Show($"Lỗi nghiệp vụ khi tìm kiếm khách hàng: {bllEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException argEx) // Bắt lỗi đối số không hợp lệ từ BLL
            {
                MessageBox.Show($"Lỗi nhập liệu: {argEx.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Lấy từ khóa hiện tại trong TextBox
            string searchTerm = txtSearch.Text.Trim();

            // Gọi phương thức tải dữ liệu đã lọc
            LoadFilteredData(searchTerm);
        }

        // Sự kiện khi click vào một dòng trong DataGridView
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải click vào một dòng dữ liệu hợp lệ không (loại trừ header).
            if (e.RowIndex >= 0 && e.RowIndex < dgvKhachHang.Rows.Count) // Đảm bảo dòng click là hợp lệ
            {
                // Lấy đối tượng Khachhang được liên kết với dòng.
                Khachhang selectedKhachhang = dgvKhachHang.Rows[e.RowIndex].DataBoundItem as Khachhang;

                // Kiểm tra xem selectedKhachhang có phải là một đối tượng Khachhang hợp lệ không.
                if (selectedKhachhang != null)
                {
                    try
                    {

                        FormChitiet formChiTiet = new FormChitiet(selectedKhachhang); // Truyền đối tượng Khachhang

                        // Hiển thị Form Chi Tiết dưới dạng Dialog
                        if (formChiTiet.ShowDialog() == DialogResult.OK)
                        {

                            LoadDanhSachKhachHang();
                        }
                    }
                    catch (InvalidOperationException bllEx)
                    {
                        MessageBox.Show("Lỗi nghiệp vụ khi lấy thông tin chi tiết khách hàng hoặc mở Form: " + bllEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentException argEx)
                    {
                        MessageBox.Show("Lỗi dữ liệu khi lấy thông tin chi tiết khách hàng: " + argEx.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lấy thông tin chi tiết khách hàng hoặc mở Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo một instance mới của Form Chi Tiết ở chế độ Thêm mới (không truyền đối tượng Khachhang)
                FormChitiet formChiTiet = new FormChitiet();

                // Hiển thị Form Chi Tiết dưới dạng Dialog
                if (formChiTiet.ShowDialog() == DialogResult.OK)
                {
                    // Nếu Form Chi Tiết trả về DialogResult.OK (nghĩa là đã thêm thành công)
                    // Tải lại danh sách khách hàng trên Form chính để hiển thị dữ liệu mới
                    LoadDanhSachKhachHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Form thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}