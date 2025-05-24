using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Đảm bảo các namespace này được tham chiếu đúng trong project của bạn
using CoffeeManagementSystem.DAL; // Chứa LoaidouongDAL, DouongDAL, GiadouongDAL
using CoffeeManagementSystem; // Chứa các lớp Model: Loaidouong, Douong, Giadouong, và AddDrinkForm, LoaidouongDetailForm

namespace CoffeeManagementSystem // Đảm bảo namespace này khớp với namespace của Form DrinkForm
{
    public partial class DrinkForm : Form
    {
        // Khai báo các đối tượng DAL
        private LoaidouongDAL loaidouongDAL = new LoaidouongDAL();
        private DouongDAL douongDAL = new DouongDAL();

        public DrinkForm()
        {
            InitializeComponent();

            // Gán sự kiện Load cho Form chính
            this.Load += DrinkForm_Load;

            // =====================================================
            // Cấu hình DataGridView và gán sự kiện cho Tab Loại đồ uống
            // (Giả định các control này nằm trên tabPageLoaidouong)
            // =====================================================
            dgvLoaidouong.AutoGenerateColumns = false;
            dgvLoaidouong.AllowUserToAddRows = false;
            dgvLoaidouong.AllowUserToDeleteRows = false;
            dgvLoaidouong.EditMode = DataGridViewEditMode.EditProgrammatically; // Không cho phép chỉnh sửa trực tiếp trên DGV

            // Gán sự kiện cho các control trên tab Loại đồ uống
            this.txtTimkiemloaidouong.TextChanged += new EventHandler(txtTimkiemloaidouong_TextChanged);
            this.btnThemloaidouong.Click += new EventHandler(btnThem_Click);
            this.dgvLoaidouong.CellClick += new DataGridViewCellEventHandler(dgvLoaidouong_CellClick);

            // =====================================================
            // Cấu hình DataGridView và gán sự kiện cho Tab Đồ uống
            // (Giả định các control này nằm trên tabPageDouong)
            // =====================================================
            dgvDouong.AutoGenerateColumns = false;
            dgvDouong.AllowUserToAddRows = false;
            dgvDouong.AllowUserToDeleteRows = false;
            dgvDouong.EditMode = DataGridViewEditMode.EditProgrammatically; // Không cho phép chỉnh sửa trực tiếp trên DGV

            // Gán sự kiện cho các control trên tab Đồ uống
            this.txtTimkiemdouong.TextChanged += new EventHandler(txtTimkiemdouong_TextChanged);
            this.btnThemdouong.Click += new EventHandler(btnAddDouong_Click);
            this.dgvDouong.CellClick += new DataGridViewCellEventHandler(dgvDouong_CellClick);
        }

        // Sự kiện Form Load: Tải dữ liệu khi Form được hiển thị
        private void DrinkForm_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu cho cả hai tab khi Form chính tải
            LoadDanhSachLoaidouong();
            LoadDanhSachDouong();
        }

        // =====================================================
        // CÁC PHƯƠNG THỨC VÀ SỰ KIỆN CHO TAB LOẠI ĐỒ UỐNG
        // =====================================================

        /// <summary>
        /// Tải danh sách loại đồ uống và hiển thị lên DataGridView.
        /// </summary>
        private void LoadDanhSachLoaidouong()
        {
            try
            {
                List<Loaidouong> danhSach = loaidouongDAL.GetAllLoaidouongs();
                dgvLoaidouong.DataSource = null; // Clear old data
                dgvLoaidouong.DataSource = danhSach;
                dgvLoaidouong.Refresh();
                dgvLoaidouong.ClearSelection(); // Xóa chọn dòng
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách loại đồ uống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách loại đồ uống đã lọc và hiển thị lên DataGridView.
        /// </summary>
        private void LoadFilteredLoaidouongData(string searchTerm)
        {
            try
            {
                List<Loaidouong> ketQuaHienThi;
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    ketQuaHienThi = loaidouongDAL.GetAllLoaidouongs();
                }
                else
                {
                    ketQuaHienThi = loaidouongDAL.SearchLoaidouongs(searchTerm);
                }

                dgvLoaidouong.DataSource = null;
                dgvLoaidouong.DataSource = ketQuaHienThi;
                dgvLoaidouong.Refresh();
                dgvLoaidouong.ClearSelection(); // Xóa chọn dòng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện TextChanged của ô tìm kiếm loại đồ uống.
        /// </summary>
        private void txtTimkiemloaidouong_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimkiemloaidouong.Text.Trim();
            LoadFilteredLoaidouongData(searchTerm);
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Thêm mới" loại đồ uống.
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Mở LoaidouongDetailForm ở chế độ thêm mới
            AddTypeofdrinkForm detailForm = new AddTypeofdrinkForm();
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachLoaidouong(); // Tải lại danh sách sau khi thêm mới thành công
            }
        }

        /// <summary>
        /// Xử lý sự kiện click vào dòng DataGridView loại đồ uống để mở form chi tiết.
        /// </summary>
        private void dgvLoaidouong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLoaidouong.Rows.Count - (dgvLoaidouong.AllowUserToAddRows ? 1 : 0))
            {
                // Lấy đối tượng Loaidouong từ dòng được click
                Loaidouong selectedLoaidouong = dgvLoaidouong.Rows[e.RowIndex].DataBoundItem as Loaidouong;

                if (selectedLoaidouong != null)
                {
                    // Mở LoaidouongDetailForm ở chế độ chỉnh sửa
                    AddTypeofdrinkForm detailForm = new AddTypeofdrinkForm(selectedLoaidouong.Maloai);
                    if (detailForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDanhSachLoaidouong(); // Tải lại danh sách sau khi chỉnh sửa/xóa thành công
                    }
                }
            }
        }


        // =====================================================
        // CÁC PHƯƠNG THỨC VÀ SỰ KIỆN CHO TAB ĐỒ UỐNG
        // =====================================================

        /// <summary>
        /// Tải danh sách đồ uống và hiển thị lên DataGridView.
        /// </summary>
        private void LoadDanhSachDouong()
        {
            try
            {
                List<Douong> danhSach = douongDAL.GetAllDouongs(); // DouongDAL đã được sửa để điền CurrentGia
                dgvDouong.DataSource = null; // Clear old data
                dgvDouong.DataSource = danhSach;
                dgvDouong.Refresh();
                dgvDouong.ClearSelection(); // Xóa chọn dòng
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách đồ uống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh sách đồ uống đã lọc và hiển thị lên DataGridView.
        /// </summary>
        private void LoadFilteredDouongData(string searchTerm)
        {
            try
            {
                List<Douong> ketQuaHienThi;
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    ketQuaHienThi = douongDAL.GetAllDouongs();
                }
                else
                {
                    ketQuaHienThi = douongDAL.SearchDouongs(searchTerm);
                }

                dgvDouong.DataSource = null;
                dgvDouong.DataSource = ketQuaHienThi;
                dgvDouong.Refresh();
                dgvDouong.ClearSelection(); // Xóa chọn dòng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện TextChanged của ô tìm kiếm đồ uống.
        /// </summary>
        private void txtTimkiemdouong_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimkiemdouong.Text.Trim();
            LoadFilteredDouongData(searchTerm);
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Thêm mới" đồ uống.
        /// </summary>
        private void btnAddDouong_Click(object sender, EventArgs e)
        {
            // Mở AddDrinkForm ở chế độ thêm mới
            AddDrinkForm detailForm = new AddDrinkForm(); // Sử dụng tên AddDrinkForm
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachDouong(); // Tải lại danh sách sau khi thêm mới thành công
            }
        }

        /// <summary>
        /// Xử lý sự kiện click vào dòng DataGridView đồ uống để mở form chi tiết.
        /// </summary>
        private void dgvDouong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDouong.Rows.Count - (dgvDouong.AllowUserToAddRows ? 1 : 0))
            {
                // Lấy đối tượng Douong từ dòng được click
                Douong selectedDouong = dgvDouong.Rows[e.RowIndex].DataBoundItem as Douong;

                if (selectedDouong != null)
                {
                    // Mở AddDrinkForm ở chế độ chỉnh sửa
                    AddDrinkForm detailForm = new AddDrinkForm(selectedDouong.Madouong); // Sử dụng tên AddDrinkForm
                    if (detailForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDanhSachDouong(); // Tải lại danh sách sau khi chỉnh sửa/xóa thành công
                    }
                }
            }
        }
    }
}
