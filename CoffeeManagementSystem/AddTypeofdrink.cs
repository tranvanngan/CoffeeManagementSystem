using System;
using System.Windows.Forms;
using CoffeeManagementSystem.DAL;
using CoffeeManagementSystem;

namespace CoffeeManagementSystem // Đảm bảo namespace này khớp với namespace của Form và các Model
{
    public partial class AddTypeofdrinkForm : Form // Đổi tên lớp Form thành AddTypeofdrinkForm
    {
        private LoaidouongDAL loaidouongDAL = new LoaidouongDAL();
        private Loaidouong _currentLoaidouong; // Đối tượng loại đồ uống hiện tại
        private bool _isNewEntry = false; // Cờ hiệu xác định là thêm mới hay chỉnh sửa

        // Constructor cho trường hợp thêm mới
        public AddTypeofdrinkForm()
        {
            InitializeComponent();
            _isNewEntry = true;
            this.Text = "Thêm Loại Đồ Uống Mới";
            txtMaloai.Enabled = true; // Cho phép nhập Mã loại khi thêm mới

            // Gán sự kiện cho các nút
            btnLuu.Click += btnLuu_Click; // Nút "Lưu"
            // btnCancel.Click += btnCancel_Click; // Removed: Nút "Hủy"
            btnCapNhat.Click += btnCapNhat_Click; // Nút "Cập nhật"
            btnXoa.Click += btnXoa_Click; // Nút "Xóa"

            // Đặt trạng thái nút ban đầu cho chế độ thêm mới
            SetButtonState(true, false, false); // Lưu enabled, Cập nhật/Xóa disabled
        }

        // Constructor cho trường hợp chỉnh sửa
        public AddTypeofdrinkForm(string maloai)
        {
            InitializeComponent();
            _isNewEntry = false;
            this.Text = "Chi Tiết Loại Đồ Uống";
            txtMaloai.Enabled = false; // Không cho phép chỉnh sửa Mã loại khi cập nhật

            // Gán sự kiện cho các nút
            btnLuu.Click += btnLuu_Click; // Nút "Lưu"
            // btnCancel.Click += btnCancel_Click; // Removed: Nút "Hủy"
            btnCapNhat.Click += btnCapNhat_Click; // Nút "Cập nhật"
            btnXoa.Click += btnXoa_Click; // Nút "Xóa"

            LoadLoaidouongDetails(maloai);

            // Đặt trạng thái nút ban đầu cho chế độ chỉnh sửa
            SetButtonState(false, true, true); // Lưu disabled, Cập nhật/Xóa enabled
        }

        /// <summary>
        /// Tải thông tin chi tiết loại đồ uống vào các control.
        /// </summary>
        /// <param name="maloai">Mã loại đồ uống cần tải.</param>
        private void LoadLoaidouongDetails(string maloai)
        {
            try
            {
                _currentLoaidouong = loaidouongDAL.GetLoaidouongById(maloai);
                if (_currentLoaidouong != null)
                {
                    txtMaloai.Text = _currentLoaidouong.Maloai;
                    txtTenloai.Text = _currentLoaidouong.Tenloai;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại đồ uống này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Lưu" (chỉ dùng cho thêm mới).
        /// </summary>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Nút "Lưu" chỉ hoạt động khi ở chế độ thêm mới
            if (!_isNewEntry)
            {
                MessageBox.Show("Vui lòng sử dụng nút 'Cập nhật' để sửa thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtMaloai.Text) || string.IsNullOrEmpty(txtTenloai.Text))
            {
                MessageBox.Show("Mã loại và Tên loại không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Loaidouong newLoai = new Loaidouong
                {
                    Maloai = txtMaloai.Text.Trim(),
                    Tenloai = txtTenloai.Text.Trim()
                };
                loaidouongDAL.AddLoaidouong(newLoai);
                MessageBox.Show("Thêm loại đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt DialogResult là OK khi lưu thành công
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Cập nhật" (chỉ dùng cho chỉnh sửa).
        /// </summary>
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Nút "Cập nhật" chỉ hoạt động khi không ở chế độ thêm mới
            if (_isNewEntry)
            {
                MessageBox.Show("Vui lòng sử dụng nút 'Lưu' để thêm mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_currentLoaidouong == null)
            {
                MessageBox.Show("Không có loại đồ uống nào được chọn để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtTenloai.Text))
            {
                MessageBox.Show("Tên loại không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _currentLoaidouong.Tenloai = txtTenloai.Text.Trim();
                loaidouongDAL.UpdateLoaidouong(_currentLoaidouong);
                MessageBox.Show("Cập nhật loại đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt DialogResult là OK khi cập nhật thành công
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Xóa" (chỉ dùng cho chỉnh sửa).
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Nút "Xóa" chỉ hoạt động khi không ở chế độ thêm mới
            if (_isNewEntry)
            {
                MessageBox.Show("Không thể xóa khi đang thêm mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_currentLoaidouong == null)
            {
                MessageBox.Show("Không có loại đồ uống nào được chọn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại đồ uống '{_currentLoaidouong.Tenloai}' (Mã: {_currentLoaidouong.Maloai}) không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    loaidouongDAL.DeleteLoaidouong(_currentLoaidouong.Maloai);
                    MessageBox.Show("Xóa loại đồ uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Đặt DialogResult là OK để Form cha biết cần tải lại dữ liệu
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa loại đồ uống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Removed: Phương thức btnCancel_Click vì không có nút Hủy

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
