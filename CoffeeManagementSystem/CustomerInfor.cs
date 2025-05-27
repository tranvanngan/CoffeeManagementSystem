﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagementSystem.BLL; // THAY THẾ using DAL bằng using BLL
// using CoffeeManagementSystem.DAL; // BỎ DÒNG NÀY

namespace CoffeeManagementSystem
{
    public partial class FormChitiet : Form
    {
        private KhachhangBLL khachhangBLL; // Thay đổi từ KhachhangDAL sang KhachhangBLL
        private Khachhang currentKhachhang; // Đối tượng Khachhang hiện tại (null nếu thêm mới)

        // Constructor cho chế độ Thêm mới
        public FormChitiet()
        {
            InitializeComponent(); // GIỮ NGUYÊN DÒNG NÀY - Nó được tạo bởi Designer
            khachhangBLL = new KhachhangBLL(); // Khởi tạo đối tượng BLL

            this.Text = "Thêm Khách Hàng Mới"; // Tiêu đề Form

            // Hiển thị tất cả các nút
            btnSave.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;

            // Điều chỉnh trạng thái Enabled cho chế độ Thêm mới
            btnSave.Enabled = true;
            btnUpdate.Enabled = false; // Không thể cập nhật khi thêm mới
            btnDelete.Enabled = false; // Không thể xóa khi thêm mới

            // Cho phép nhập Mã KH khi thêm mới
            txtMaKH.Enabled = true;
        }

        // Constructor cho chế độ Cập nhật
        public FormChitiet(Khachhang khachhangToEdit)
        {
            InitializeComponent(); // GIỮ NGUYÊN DÒNG NÀY - Nó được tạo bởi Designer
            khachhangBLL = new KhachhangBLL(); // Khởi tạo đối tượng BLL

            this.Text = "Cập Nhật Thông Tin Khách Hàng"; // Tiêu đề Form
            currentKhachhang = khachhangToEdit; // Lưu đối tượng khách hàng cần sửa

            // Hiển thị tất cả các nút
            btnSave.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;

            // Điều chỉnh trạng thái Enabled cho chế độ Cập nhật
            btnSave.Enabled = false; // Không thể lưu mới khi cập nhật
            btnUpdate.Enabled = true; // Có thể cập nhật
            btnDelete.Enabled = true; // Có thể xóa

            // Vô hiệu hóa ô Mã KH khi sửa
            txtMaKH.Enabled = false;

            // Hiển thị thông tin khách hàng lên các control
            DisplayKhachhangInfo();
        }

        // Phương thức hiển thị thông tin khách hàng lên các control
        private void DisplayKhachhangInfo()
        {
            if (currentKhachhang != null)
            {
                txtMaKH.Text = currentKhachhang.Makhachhang;
                txtHoTen.Text = currentKhachhang.Hoten;
                txtSDT.Text = currentKhachhang.Sodienthoai;
                txtEmail.Text = currentKhachhang.Email;
                dateTimePickerNgayDangKy.Value = currentKhachhang.Ngaydangky;
                numericUpDownDiem.Value = currentKhachhang.Diemtichluy;
            }
        }

        // Phương thức lấy thông tin từ các control và tạo/cập nhật đối tượng Khachhang
        private Khachhang GetKhachhangInfoFromControls()
        {
            // Tạo đối tượng Khachhang mới hoặc sử dụng currentKhachhang nếu đang sửa
            Khachhang khachhang = currentKhachhang ?? new Khachhang();

            // Mã khách hàng chỉ được lấy từ TextBox khi THÊM MỚI
            // Khi CẬP NHẬT, Makhachhang đã có từ currentKhachhang (và txtMaKH bị disable)
            if (currentKhachhang == null) // Chế độ thêm mới
            {
                khachhang.Makhachhang = txtMaKH.Text.Trim();
            }
            // else: Makhachhang đã có giá trị từ currentKhachhang và không cần thay đổi từ txtMaKH

            khachhang.Hoten = txtHoTen.Text.Trim();
            // Xử lý các cột có thể NULL
            string sdt = txtSDT.Text.Trim();
            khachhang.Sodienthoai = string.IsNullOrEmpty(sdt) ? null : sdt;
            string email = txtEmail.Text.Trim();
            khachhang.Email = string.IsNullOrEmpty(email) ? null : email;
            // Lấy giá trị từ DateTimePicker
            khachhang.Ngaydangky = dateTimePickerNgayDangKy.Value;
            // Lấy giá trị từ NumericUpDown
            khachhang.Diemtichluy = (int)numericUpDownDiem.Value;

            return khachhang;
        }

        // Sự kiện click nút "Lưu" (cho chế độ Thêm mới)
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các control
            Khachhang newKhachhang = GetKhachhangInfoFromControls();

            // Kiểm tra dữ liệu bắt buộc (ví dụ: Mã KH, Họ tên) - Có thể chuyển logic này vào BLL
            if (string.IsNullOrEmpty(newKhachhang.Makhachhang) || string.IsNullOrEmpty(newKhachhang.Hoten))
            {
                MessageBox.Show("Mã khách hàng và Họ tên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                khachhangBLL.AddKhachhang(newKhachhang); // Gọi phương thức Thêm từ BLL
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt kết quả Form là OK
                this.Close(); // Đóng Form
            }
            catch (ArgumentException argEx) // Bắt lỗi đối số không hợp lệ từ BLL
            {
                MessageBox.Show($"Lỗi nhập liệu: {argEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException invOpEx) // Bắt lỗi nghiệp vụ từ BLL
            {
                MessageBox.Show($"Lỗi nghiệp vụ: {invOpEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click nút "Cập Nhật" (cho chế độ Sửa)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Lấy thông tin cập nhật từ các control
            // Sử dụng currentKhachhang để giữ nguyên Mã KH (đã xử lý trong GetKhachhangInfoFromControls)
            Khachhang updatedKhachhang = GetKhachhangInfoFromControls();

            // Kiểm tra dữ liệu bắt buộc (ví dụ: Họ tên) - Có thể chuyển logic này vào BLL
            if (string.IsNullOrEmpty(updatedKhachhang.Hoten))
            {
                MessageBox.Show("Họ tên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                khachhangBLL.UpdateKhachhang(updatedKhachhang); // Gọi phương thức Cập nhật từ BLL
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt kết quả Form là OK
                this.Close(); // Đóng Form
            }
            catch (ArgumentException argEx) // Bắt lỗi đối số không hợp lệ từ BLL
            {
                MessageBox.Show($"Lỗi nhập liệu: {argEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException invOpEx) // Bắt lỗi nghiệp vụ từ BLL
            {
                MessageBox.Show($"Lỗi nghiệp vụ: {invOpEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện click nút "Xóa"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentKhachhang == null)
            {
                MessageBox.Show("Không có khách hàng nào được chọn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng '{currentKhachhang.Hoten}' (Mã: {currentKhachhang.Makhachhang}) không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    khachhangBLL.DeleteKhachhang(currentKhachhang.Makhachhang); // Gọi phương thức Xóa từ BLL
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Đặt kết quả Form là OK (để Form cha biết cần tải lại dữ liệu)
                    this.Close(); // Đóng Form
                }
                catch (ArgumentException argEx) // Bắt lỗi đối số không hợp lệ từ BLL
                {
                    MessageBox.Show($"Lỗi nhập liệu: {argEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (InvalidOperationException invOpEx) // Bắt lỗi nghiệp vụ từ BLL
                {
                    MessageBox.Show($"Lỗi nghiệp vụ: {invOpEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sự kiện click nút đóng Form (X) ở góc trên bên phải
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Đặt kết quả Form là Cancel
            this.Close(); // Đóng Form
        }
    }
}