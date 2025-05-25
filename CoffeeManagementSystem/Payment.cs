using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeManagementSystem.DAL; // Đảm bảo đã using namespace chứa DonhangDAL, ChitietdonhangDAL, KhachhangDAL

namespace CoffeeManagementSystem
{
    public partial class PaymentForm : Form
    {
        private List<Chitietdonhang> _dsChiTietHoaDon;
        private string _manhanvienLapHoaDon;
        private string _tenNhanVienLapHoaDon;
        private string _maHoaDonHienTai;
        private Khachhang currentSelectedCustomer;

        // Khai báo các đối tượng DAL
        private DonhangDAL donhangDAL = new DonhangDAL();
        private ChitietdonhangDAL chitietdonhangDAL = new ChitietdonhangDAL();
        private KhachhangDAL khachhangDAL = new KhachhangDAL();

        // Constructor nhận danh sách Chitietdonhang, Mã nhân viên và Tên nhân viên
        public PaymentForm(List<Chitietdonhang> dsChiTiet, string manhanvien, string tenNhanVien)
        {
            InitializeComponent();
            _dsChiTietHoaDon = dsChiTiet;
            _manhanvienLapHoaDon = manhanvien;
            _tenNhanVienLapHoaDon = tenNhanVien;

            this.Text = "Payment";

            // Gán sự kiện Load cho Form
            this.Load += PaymentForm_Load;
            // Gán sự kiện Click cho nút Thanh toán (đảm bảo tên nút là btnThanhToan)
            this.btnThanhToan.Click += btnThanhToan_Click;
        }

        // Phương thức Load của Form (sẽ được gọi khi Form được tải)
        private void PaymentForm_Load(object sender, EventArgs e)
        {
            // Tạo mã hóa đơn và hiển thị thông tin chung
            _maHoaDonHienTai = GenerateUniqueDonhangId();
            lblMaHoaDonValue.Text = _maHoaDonHienTai;
            lblNguoiLapValue.Text = _tenNhanVienLapHoaDon;
            lblNgayValue.Text = DateTime.Now.ToShortDateString();

            SetupListViewColumns();
            LoadChiTietHoaDon();
            TinhTongTien();
        }

        /// <summary>
        /// Thiết lập các cột cho ListView hiển thị chi tiết đơn hàng.
        /// </summary>
        private void SetupListViewColumns()
        {
            if (lvwChiTietHoaDon != null)
            {
                lvwChiTietHoaDon.View = View.Details;
                lvwChiTietHoaDon.GridLines = true;
                lvwChiTietHoaDon.FullRowSelect = true;

                lvwChiTietHoaDon.Columns.Clear();

                lvwChiTietHoaDon.Columns.Add("STT", 50, HorizontalAlignment.Center);
                lvwChiTietHoaDon.Columns.Add("Tên đồ uống", 200, HorizontalAlignment.Left);
                lvwChiTietHoaDon.Columns.Add("Số lượng", 80, HorizontalAlignment.Center);
                lvwChiTietHoaDon.Columns.Add("Đơn giá", 100, HorizontalAlignment.Right);
                lvwChiTietHoaDon.Columns.Add("Thành tiền", 120, HorizontalAlignment.Right);
            }
        }

        /// <summary>
        /// Tải dữ liệu chi tiết đơn hàng (từ danh sách tạm thời) vào ListView.
        /// Sử dụng Tendouong đã được gán từ OrderForm.
        /// </summary>
        private void LoadChiTietHoaDon()
        {
            if (lvwChiTietHoaDon != null)
            {
                lvwChiTietHoaDon.Items.Clear();

                for (int i = 0; i < _dsChiTietHoaDon.Count; i++)
                {
                    Chitietdonhang chiTiet = _dsChiTietHoaDon[i];

                    ListViewItem lvi = new ListViewItem((i + 1).ToString());
                    lvi.SubItems.Add(chiTiet.Tendouong);
                    lvi.SubItems.Add(chiTiet.Soluong.ToString());
                    lvi.SubItems.Add(chiTiet.Dongia.ToString("N0"));
                    lvi.SubItems.Add(chiTiet.Thanhtien.ToString("N0"));

                    lvwChiTietHoaDon.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// Tính toán và hiển thị tổng thành tiền.
        /// </summary>
        private void TinhTongTien()
        {
            decimal tongTien = _dsChiTietHoaDon.Sum(item => item.Thanhtien);
            txtTongThanhTienValue.Text = tongTien.ToString("N0");
        }

        /// <summary>
        /// Xử lý sự kiện click nút "Thanh toán".
        /// </summary>
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_dsChiTietHoaDon.Count == 0)
            {
                MessageBox.Show("Không có đồ uống nào trong hóa đơn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn thanh toán đơn hàng này không?", "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    decimal tongTienHoaDon = _dsChiTietHoaDon.Sum(item => item.Thanhtien);

                    string makhachhang = null;
                    if (!string.IsNullOrWhiteSpace(txtKhachHangName.Text))
                    {
                        // Logic tìm kiếm khách hàng theo tên hoặc số điện thoại (nếu có)
                        // Ví dụ: Khachhang kh = khachhangDAL.GetKhachhangByTenHoacSDT(txtKhachHangName.Text);
                        // if (kh != null) { makhachhang = kh.Makhachhang; }
                    }

                    Donhang newDonhang = new Donhang
                    {
                        Madonhang = _maHoaDonHienTai,
                        Manhanvien = _manhanvienLapHoaDon,
                        Makhachhang = makhachhang,
                        Thoigiandat = DateTime.Now,
                        Trangthaidon = "Hoàn thành",
                        Tongtien = tongTienHoaDon
                    };

                    donhangDAL.AddDonhang(newDonhang);

                    // Lưu từng chi tiết đơn hàng (Chitietdonhang)
                    foreach (var item in _dsChiTietHoaDon)
                    {
                        item.Madonhang = _maHoaDonHienTai;
                        // KHÔNG GÁN Machitiet ở đây nữa
                        chitietdonhangDAL.AddChitietdonhang(item);
                    }

                    MessageBox.Show("Đơn hàng đã được thanh toán và lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thanh toán đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void txtKhachHangName_Leave(object sender, EventArgs e)
        {
            string customerName = txtKhachHangName.Text.Trim();

            // Nếu tên khách hàng rỗng, không làm gì cả
            if (string.IsNullOrEmpty(customerName))
            {
                ClearCustomerInfo(); // Xóa thông tin khách hàng nếu tên rỗng
                return;
            }

            try
            {
                // 1. Tìm kiếm khách hàng theo tên
                Khachhang existingCustomer = khachhangDAL.GetKhachhangByName(customerName);

                if (existingCustomer == null)
                {
                    // 2. Nếu khách hàng chưa tồn tại, hỏi người dùng có muốn thêm mới không
                    DialogResult confirmResult = MessageBox.Show(
                        $"Khách hàng '{customerName}' chưa tồn tại. Bạn có muốn thêm mới khách hàng này không?",
                        "Xác nhận thêm khách hàng",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirmResult == DialogResult.Yes)
                    {
                        Khachhang newCustomer = new Khachhang
                        {
                            Makhachhang = GenerateUniqueKhachhangId(), // Tạo mã khách hàng duy nhất
                            Hoten = customerName,
                            Ngaydangky = DateTime.Now, // Ngày đăng ký là thời điểm hiện tại
                            Diemtichluy = 0, // Điểm tích lũy ban đầu là 0
                            // Các trường khác như Sodienthoai, Email sẽ là null/default
                            // vì chúng không được yêu cầu trong DAL AddKhachhang của bạn.
                            // Nếu bạn có một txtCustomerPhone, bạn có thể lấy giá trị từ đó:
                            // Sodienthoai = txtCustomerPhone.Text.Trim()
                        };

                        khachhangDAL.AddKhachhang(newCustomer);
                        currentSelectedCustomer = newCustomer; // Gán khách hàng mới làm khách hàng hiện tại
                        MessageBox.Show($"Đã thêm mới khách hàng: {customerName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Cập nhật UI nếu có (ví dụ: hiển thị điểm tích lũy, thông báo)
                        
                    }
                    else
                    {
                        // Nếu người dùng không muốn thêm mới, xóa tên đã nhập
                        txtKhachHangName.Text = "";
                        ClearCustomerInfo();
                    }
                }
                else
                {
                    // 3. Nếu khách hàng đã tồn tại, gán làm khách hàng hiện tại
                    currentSelectedCustomer = existingCustomer;
                    MessageBox.Show($"Khách hàng '{customerName}' đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearCustomerInfo(); // Xóa thông tin khách hàng nếu có lỗi
            }
        }
        private void ClearCustomerInfo()
        {
            currentSelectedCustomer = null;
            // Ví dụ: Xóa nội dung Label điểm tích lũy
            // lblDiemTichLuy.Text = "Điểm tích lũy: 0";
            // Nếu bạn có một TextBox cho số điện thoại:
            // txtCustomerPhone.Text = "";
        }


        /// <summary>
        /// Helper method để tạo ID duy nhất cho Madonhang.
        /// </summary>
        private string GenerateUniqueDonhangId()
        {
            return "DH" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private string GenerateUniqueKhachhangId()
        {
            return "KH" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}
