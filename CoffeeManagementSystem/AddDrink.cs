using System;
using System.Windows.Forms;
using System.Collections.Generic;
using CoffeeManagementSystem.DAL;
using CoffeeManagementSystem;
using System.IO; // Required for File.Exists

namespace CoffeeManagementSystem // Đảm bảo namespace này khớp với namespace của Form và các Model
{
    public partial class AddDrinkForm : Form // Đã đổi tên lớp Form thành AddDrinkForm
    {
        private DouongDAL douongDAL = new DouongDAL();
        private GiadouongDAL giadouongDAL = new GiadouongDAL();
        private LoaidouongDAL loaidouongDAL = new LoaidouongDAL(); // Cần để tải ComboBox loại đồ uống

        private Douong _currentDouong; // Đối tượng đồ uống hiện tại
        private bool _isNewEntry = false; // Cờ hiệu xác định là thêm mới hay chỉnh sửa
        private string _selectedImagePath = ""; // Biến để lưu trữ đường dẫn ảnh được chọn

        // Constructor cho trường hợp thêm mới
        public AddDrinkForm()
        {
            InitializeComponent();
            _isNewEntry = true;
            this.Text = "Thêm Đồ Uống Mới";
            txtMadouong.Enabled = true; // Cho phép nhập Mã đồ uống khi thêm mới
            LoadLoaiDouongComboBox(); // Tải ComboBox loại đồ uống

            // Gán sự kiện cho các nút
            btnLuu.Click += btnLuu_Click; // Nút "Lưu"
            btnCapNhat.Click += btnCapNhat_Click; // Nút "Cập nhật"
            btnXoa.Click += btnXoa_Click; // Nút "Xóa"
            btnSelectImage.Click += btnSelectImage_Click; // Nút "Chọn" ảnh

            // Khởi tạo trạng thái cho PictureBox và đường dẫn ảnh
            pbHinhanh.Image = null;
            _selectedImagePath = "";

            // Đặt trạng thái nút ban đầu cho chế độ thêm mới
            SetButtonState(true, false, false); // Lưu enabled, Cập nhật/Xóa disabled
        }

        // Constructor cho trường hợp chỉnh sửa
        public AddDrinkForm(string madouong)
        {
            InitializeComponent();
            _isNewEntry = false;
            this.Text = "Chi Tiết Đồ Uống";
            txtMadouong.Enabled = false; // Không cho phép chỉnh sửa Mã đồ uống khi cập nhật
            LoadLoaiDouongComboBox(); // Tải ComboBox loại đồ uống

            // Gán sự kiện cho các nút
            btnLuu.Click += btnLuu_Click; // Nút "Lưu"
            btnCapNhat.Click += btnCapNhat_Click; // Nút "Cập nhật"
            btnXoa.Click += btnXoa_Click; // Nút "Xóa"
            btnSelectImage.Click += btnSelectImage_Click; // Nút "Chọn" ảnh

            LoadDouongDetails(madouong);

            // Đặt trạng thái nút ban đầu cho chế độ chỉnh sửa
            SetButtonState(false, true, true); // Lưu disabled, Cập nhật/Xóa enabled
        }

        /// <summary>
        /// Tải danh sách loại đồ uống vào ComboBox.
        /// </summary>
        private void LoadLoaiDouongComboBox()
        {
            try
            {
                List<Loaidouong> loaiDouongs = loaidouongDAL.GetAllLoaidouongs();
                cbLoaiDouong.DataSource = loaiDouongs;
                cbLoaiDouong.DisplayMember = "Tenloai";
                cbLoaiDouong.ValueMember = "Maloai";
                cbLoaiDouong.SelectedIndex = -1; // Không chọn gì ban đầu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách loại đồ uống vào ComboBox: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải thông tin chi tiết đồ uống vào các control.
        /// </summary>
        /// <param name="madouong">Mã đồ uống cần tải.</param>
        private void LoadDouongDetails(string madouong)
        {
            try
            {
                _currentDouong = douongDAL.GetDouongById(madouong); // Phương thức này đã điền CurrentGia
                if (_currentDouong != null)
                {
                    txtMadouong.Text = _currentDouong.Madouong;
                    txtTendouong.Text = _currentDouong.Tendouong;
                    cbLoaiDouong.SelectedValue = _currentDouong.Maloai; // Chọn loại đồ uống trong ComboBox
                    txtGiaBan.Text = _currentDouong.CurrentGia.ToString(); // Hiển thị giá hiện tại
                    txtMota.Text = _currentDouong.Mota;

                    // Tải ảnh vào PictureBox nếu đường dẫn hợp lệ
                    if (!string.IsNullOrEmpty(_currentDouong.Hinhanh) && File.Exists(_currentDouong.Hinhanh))
                    {
                        pbHinhanh.ImageLocation = _currentDouong.Hinhanh;
                        _selectedImagePath = _currentDouong.Hinhanh; // Cập nhật đường dẫn ảnh đã chọn
                    }
                    else
                    {
                        pbHinhanh.Image = null; // Xóa ảnh nếu đường dẫn không hợp lệ
                        _selectedImagePath = "";
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đồ uống này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Chọn" để chọn ảnh.
        /// </summary>
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
            openFileDialog.Title = "Chọn ảnh đồ uống";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = openFileDialog.FileName;
                pbHinhanh.ImageLocation = _selectedImagePath; // Hiển thị ảnh trong PictureBox
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Lưu" (thêm mới).
        /// </summary>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate input for price
            decimal newGia;
            if (!decimal.TryParse(txtGiaBan.Text, out newGia) || newGia < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ. Vui lòng nhập một số dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate required fields
            if (string.IsNullOrEmpty(txtMadouong.Text) || string.IsNullOrEmpty(txtTendouong.Text) || cbLoaiDouong.SelectedValue == null)
            {
                MessageBox.Show("Mã đồ uống, Tên đồ uống và Loại đồ uống không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Douong newDouong = new Douong
                {
                    Madouong = txtMadouong.Text.Trim(),
                    Tendouong = txtTendouong.Text.Trim(),
                    Maloai = cbLoaiDouong.SelectedValue.ToString(),
                    Mota = txtMota.Text.Trim(),
                    Hinhanh = _selectedImagePath // Lấy đường dẫn ảnh từ biến đã chọn
                };

                douongDAL.AddDouong(newDouong);

                // Thêm bản ghi giá ban đầu
                Giadouong initialGia = new Giadouong
                {
                    Magia = GenerateNewGiadouongId(),
                    Madouong = newDouong.Madouong,
                    Giaban = newGia,
                    Thoigianapdung = DateTime.Now
                };
                giadouongDAL.AddGiadouong(initialGia);

                MessageBox.Show("Thêm đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt DialogResult là OK khi lưu thành công
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Cập nhật".
        /// </summary>
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (_currentDouong == null)
            {
                MessageBox.Show("Không có đồ uống nào được chọn để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy giá trị mới từ các TextBox
            string newTendouong = txtTendouong.Text.Trim();
            string newMaloai = cbLoaiDouong.SelectedValue?.ToString();
            string newMota = txtMota.Text.Trim();
            string newHinhanh = _selectedImagePath; // Lấy đường dẫn ảnh từ biến đã chọn
            decimal newGia;

            if (!decimal.TryParse(txtGiaBan.Text, out newGia) || newGia < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ. Vui lòng nhập một số dương.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(newTendouong) || string.IsNullOrEmpty(newMaloai))
            {
                MessageBox.Show("Tên đồ uống và Loại đồ uống không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật thông tin đồ uống (trừ giá)
            _currentDouong.Tendouong = newTendouong;
            _currentDouong.Maloai = newMaloai;
            _currentDouong.Mota = newMota;
            _currentDouong.Hinhanh = newHinhanh;

            try
            {
                douongDAL.UpdateDouong(_currentDouong);

                // Kiểm tra và cập nhật giá nếu có thay đổi
                if (newGia != _currentDouong.CurrentGia)
                {
                    Giadouong newGiadouong = new Giadouong
                    {
                        Magia = GenerateNewGiadouongId(),
                        Madouong = _currentDouong.Madouong,
                        Giaban = newGia,
                        Thoigianapdung = DateTime.Now
                    };
                    giadouongDAL.AddGiadouong(newGiadouong);
                    MessageBox.Show("Đã cập nhật thông tin đồ uống và giá mới!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đã cập nhật thông tin đồ uống.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.DialogResult = DialogResult.OK; // Đặt DialogResult là OK khi lưu thành công
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Xóa".
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_currentDouong == null)
            {
                MessageBox.Show("Không có đồ uống nào được chọn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa đồ uống '{_currentDouong.Tendouong}' (Mã: {_currentDouong.Madouong}) không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    douongDAL.DeleteDouong(_currentDouong.Madouong);
                    MessageBox.Show("Xóa đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Đặt DialogResult là OK để Form cha biết cần tải lại dữ liệu
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Hủy".
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pbHinhanh.Image = null; // Xóa ảnh khi hủy
            _selectedImagePath = ""; // Xóa đường dẫn ảnh khi hủy
            this.DialogResult = DialogResult.Cancel; // Đặt DialogResult là Cancel khi hủy
            this.Close();
        }

        /// <summary>
        /// Helper method để tạo ID duy nhất cho Magia.
        /// </summary>
        private string GenerateNewGiadouongId()
        {
            return "GIA" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// Đặt trạng thái Enabled cho các nút trên Form.
        /// </summary>
        /// <param name="luuEnabled">Trạng thái Enabled của nút Lưu.</param>
        /// <param name="capNhatEnabled">Trạng thái Enabled của nút Cập nhật.</param>
        /// <param name="xoaEnabled">Trạng thái Enabled của nút Xóa.</param>
        private void SetButtonState(bool luuEnabled, bool capNhatEnabled, bool xoaEnabled)
        {
            btnLuu.Enabled = luuEnabled;
            btnCapNhat.Enabled = capNhatEnabled;
            btnXoa.Enabled = xoaEnabled;
        }
    }
}
