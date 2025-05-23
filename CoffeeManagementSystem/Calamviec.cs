using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Đảm bảo using namespace chứa các lớp Model và DAL của bạn
using CoffeeManagementSystem; // Chứa Calamviec, Nhanvien, Chamcong models
using CoffeeManagementSystem.DAL; // Chứa CalamviecDAL, NhanvienDAL, ChamcongDAL

namespace CoffeeManagementSystem // Đảm bảo namespace này khớp với dự án của bạn
{
    public partial class FormChiTietCalamviec : Form
    {
        private CalamviecDAL calamviecDAL = new CalamviecDAL();
        private NhanvienDAL nhanvienDAL = new NhanvienDAL(); // Thêm NhanvienDAL để lấy danh sách nhân viên
        private ChamcongDAL chamcongDAL = new ChamcongDAL(); // Thêm ChamcongDAL để thêm bản ghi chấm công

        private Calamviec currentCalamviec;

        // Constructor cho chế độ Thêm mới
        public FormChiTietCalamviec()
        {
            InitializeComponent();
            this.Text = "Thêm Ca Làm Việc Mới";

            btnSave.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtMaCa.Enabled = true;

            // Cấu hình DateTimePicker cho thời gian
            dateTimePickerBatDau.Format = DateTimePickerFormat.Time;
            dateTimePickerBatDau.ShowUpDown = true;
            dateTimePickerKetThuc.Format = DateTimePickerFormat.Time;
            dateTimePickerKetThuc.ShowUpDown = true;

            // Tải danh sách nhân viên vào CheckedListBox
            LoadNhanviensToCheckedListBox();
            // clbNhanvienAssigned sẽ Enabled trong chế độ thêm mới
            clbNhanvienAssigned.Enabled = true;
        }

        // Constructor cho chế độ Cập nhật (NHẬN 1 ĐỐI SỐ)
        public FormChiTietCalamviec(Calamviec calamviecToEdit) // Đây là constructor bị thiếu hoặc không đúng
        {
            InitializeComponent();
            this.Text = "Cập Nhật Thông Tin Ca Làm Việc";
            currentCalamviec = calamviecToEdit;

            btnSave.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;

            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            txtMaCa.Enabled = false;

            // Cấu hình DateTimePicker cho thời gian
            dateTimePickerBatDau.Format = DateTimePickerFormat.Time;
            dateTimePickerBatDau.ShowUpDown = true;
            dateTimePickerKetThuc.Format = DateTimePickerFormat.Time;
            dateTimePickerKetThuc.ShowUpDown = true;

            // Tải danh sách nhân viên vào CheckedListBox (nhưng sẽ vô hiệu hóa nó)
            LoadNhanviensToCheckedListBox();
            // Vô hiệu hóa CheckedListBox trong chế độ cập nhật
            clbNhanvienAssigned.Enabled = false;

            DisplayCalamviecInfo();
        }

        // Phương thức tải danh sách nhân viên vào CheckedListBox
        private void LoadNhanviensToCheckedListBox()
        {
            try
            {
                List<Nhanvien> nhanviens = nhanvienDAL.GetAllNhanviens();
                clbNhanvienAssigned.Items.Clear(); // Xóa các mục cũ

                foreach (Nhanvien nv in nhanviens)
                {
                    // Thêm nhân viên vào CheckedListBox, hiển thị Hoten và lưu trữ Manhanvien
                    clbNhanvienAssigned.Items.Add(new NhanvienDisplayItem { NhanvienId = nv.Manhanvien, NhanvienName = nv.Hoten });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lớp helper để hiển thị tên và lưu trữ ID trong CheckedListBox
        private class NhanvienDisplayItem
        {
            public string NhanvienId { get; set; }
            public string NhanvienName { get; set; }

            public override string ToString()
            {
                return NhanvienName; // Chỉ hiển thị tên trong CheckedListBox
            }
        }


        private void DisplayCalamviecInfo()
        {
            if (currentCalamviec != null)
            {
                txtMaCa.Text = currentCalamviec.Maca;
                txtTenCa.Text = currentCalamviec.Tenca;
                dateTimePickerBatDau.Value = DateTime.Today.Add(currentCalamviec.Thoigianbatdau);
                dateTimePickerKetThuc.Value = DateTime.Today.Add(currentCalamviec.Thoigianketthuc);
                numericUpDownSoGio.Value = currentCalamviec.Sogio;
            }
        }

        private Calamviec GetCalamviecInfoFromControls()
        {
            Calamviec calamviec = currentCalamviec ?? new Calamviec();

            if (currentCalamviec == null)
            {
                calamviec.Maca = txtMaCa.Text.Trim();
            }

            calamviec.Tenca = txtTenCa.Text.Trim();
            calamviec.Thoigianbatdau = dateTimePickerBatDau.Value.TimeOfDay;
            calamviec.Thoigianketthuc = dateTimePickerKetThuc.Value.TimeOfDay;
            calamviec.Sogio = numericUpDownSoGio.Value;

            return calamviec;
        }

        // =====================================================
        // XỬ LÝ CÁC NÚT TRÊN FORM CHI TIẾT
        // =====================================================

        private void btnSave_Click(object sender, EventArgs e)
        {
            Calamviec newCalamviec = GetCalamviecInfoFromControls();

            if (string.IsNullOrEmpty(newCalamviec.Maca) || string.IsNullOrEmpty(newCalamviec.Tenca))
            {
                MessageBox.Show("Mã ca và Tên ca không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                calamviecDAL.AddCalamviec(newCalamviec);
                MessageBox.Show("Thêm ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (clbNhanvienAssigned.Enabled)
                {
                    foreach (object item in clbNhanvienAssigned.CheckedItems)
                    {
                        NhanvienDisplayItem selectedNv = item as NhanvienDisplayItem;
                        if (selectedNv != null)
                        {
                            Chamcong chamcong = new Chamcong
                            {
                                Machamcong = GenerateUniqueChamcongId(),
                                Manhanvien = selectedNv.NhanvienId,
                                Maca = newCalamviec.Maca,
                                Ngay = DateTime.Today
                            };
                            chamcongDAL.AddChamcong(chamcong);
                        }
                    }
                    MessageBox.Show("Đã gán nhân viên cho ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm ca làm việc hoặc gán nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Calamviec updatedCalamviec = GetCalamviecInfoFromControls();

            if (string.IsNullOrEmpty(updatedCalamviec.Tenca))
            {
                MessageBox.Show("Tên ca không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                calamviecDAL.UpdateCalamviec(updatedCalamviec);
                MessageBox.Show("Cập nhật ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật ca làm việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentCalamviec == null)
            {
                MessageBox.Show("Không có ca làm việc nào được chọn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa ca làm việc '{currentCalamviec.Tenca}' (Mã: {currentCalamviec.Maca}) không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    calamviecDAL.DeleteCalamviec(currentCalamviec.Maca);
                    MessageBox.Show("Xóa ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa ca làm việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private string GenerateUniqueChamcongId()
        {
            return "CC" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private void clbNhanvienAssigned_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
