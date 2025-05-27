using CoffeeManagementSystem.DAL;
using CoffeeManagementSystem.BLL; // Thêm dòng này để sử dụng BLL
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq; // Cần thiết nếu bạn dùng LINQ (cho Sum())

namespace CoffeeManagementSystem
{
    public partial class ReportForm : Form
    {
        private ReportBLL _reportBLL; // Khai báo đối tượng BLL thay vì các DAL riêng lẻ

        public ReportForm()
        {
            InitializeComponent();

            _reportBLL = new ReportBLL(); // Khởi tạo BLL

            // Thiết lập giá trị mặc định cho DateTimePicker của báo cáo doanh thu
            dtpRevenueStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpRevenueEndDate.Value = DateTime.Now;

            // Thiết lập giá trị mặc định cho DateTimePicker của báo cáo bán hàng theo đồ uống (nếu có)
            // Giả sử bạn có dtpProductSalesStartDate và dtpProductSalesEndDate trong Designer
            // dtpProductSalesStartDate.Value = DateTime.Now.AddMonths(-1);
            // dtpProductSalesEndDate.Value = DateTime.Now;

            // Gắn sự kiện SelectedIndexChanged cho TabControl chính
            this.tabControlReports.SelectedIndexChanged += new EventHandler(this.tabControlReports_SelectedIndexChanged);
            this.dtpRevenueStartDate.ValueChanged += new EventHandler(this.dtpRevenue_ValueChanged);
            this.dtpRevenueEndDate.ValueChanged += new EventHandler(this.dtpRevenue_ValueChanged);
            this.dtpProductSalesStartDate.ValueChanged += new EventHandler(this.dtpProductSales_ValueChanged);
            this.dtpProductSalesEndDate.ValueChanged += new EventHandler(this.dtpProductSales_ValueChanged);
            // Tải báo cáo cho tab được chọn mặc định khi form được mở lần đầu
            tabControlReports_SelectedIndexChanged(tabControlReports, EventArgs.Empty);
        }
        private void dtpRevenue_ValueChanged(object sender, EventArgs e)
        {
            // Chỉ tải lại báo cáo doanh thu nếu người dùng đang ở tab doanh thu
            if (tabControlReports.SelectedTab == tabPage2) // Tab Doanh thu
            {
                LoadRevenueReport();
            }
        }
        private void dtpProductSales_ValueChanged(object sender, EventArgs e)
        {
            // Chỉ tải lại báo cáo bán hàng theo đồ uống nếu người dùng đang ở tab đó
            if (tabControlReports.SelectedTab == tabPage3)
            {
                LoadProductSalesReport();
            }
        }
        /// <summary>
        /// Xử lý sự kiện khi người dùng thay đổi TabPage trên TabControl.
        /// Tải dữ liệu báo cáo tương ứng với TabPage được chọn.
        /// </summary>
        private void tabControlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlReports.SelectedTab == tabPage1) // Tab Khách hàng tiềm năng
            {
                LoadPotentialCustomersReport();
            }
            else if (tabControlReports.SelectedTab == tabPage2) // Tab Doanh thu
            {
                LoadRevenueReport();
            }
            // Thêm điều kiện cho TabPage báo cáo bán hàng theo đồ uống
            // Đảm bảo tên TabPage của bạn trong Designer là 'tabPageProductSales'
            else if (tabControlReports.SelectedTab == tabPage3)
            {
                LoadProductSalesReport();
            }
            // Thêm các điều kiện 'else if' cho các TabPage báo cáo khác nếu có
        }

        /// <summary>
        /// Tải dữ liệu báo cáo 10 khách hàng có điểm tích lũy cao nhất và hiển thị lên DataGridView.
        /// </summary>
        private void LoadPotentialCustomersReport()
        {
            try
            {
                List<Khachhang> potentialCustomers = _reportBLL.GetPotentialCustomersReport();

                // 1. Đảm bảo dgvPotentialCustomers không có nguồn dữ liệu cũ
                dgvPotentialCustomers.DataSource = null;

                // 2. Xóa tất cả các cột cũ (đảm bảo không bị trùng lặp cột)
                dgvPotentialCustomers.Columns.Clear();

                // 3. Cấu hình cột (Chỉ cấu hình một lần khi form/DGV được tạo hoặc khi reset)
                // (Nếu bạn đã cấu hình các cột này trong Designer, bạn có thể bỏ qua bước này.
                // Tuy nhiên, vì bạn đang tạo thủ công, hãy giữ lại và đảm bảo nó được gọi một lần.)

                // Cột "STT" - KHÔNG GÁN DataPropertyName, sẽ điền thủ công hoặc qua CellFormatting
                DataGridViewTextBoxColumn sttColumn = new DataGridViewTextBoxColumn();
                sttColumn.Name = "STT";
                sttColumn.HeaderText = "STT";
                sttColumn.Width = 50;
                sttColumn.ReadOnly = true;
                sttColumn.Resizable = DataGridViewTriState.False;
                sttColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvPotentialCustomers.Columns.Add(sttColumn);

                // Các cột dữ liệu khác
                DataGridViewTextBoxColumn maKHColumn = new DataGridViewTextBoxColumn();
                maKHColumn.DataPropertyName = "Makhachhang"; // DGV sẽ tự lấy giá trị từ đây
                maKHColumn.Name = "Makhachhang";
                maKHColumn.HeaderText = "Mã Khách hàng";
                maKHColumn.Width = 120;
                maKHColumn.ReadOnly = true;
                maKHColumn.Resizable = DataGridViewTriState.False;
                maKHColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvPotentialCustomers.Columns.Add(maKHColumn);

                DataGridViewTextBoxColumn tenKHColumn = new DataGridViewTextBoxColumn();
                tenKHColumn.DataPropertyName = "Hoten"; // DGV sẽ tự lấy giá trị từ đây
                tenKHColumn.Name = "TenKhachhang";
                tenKHColumn.HeaderText = "Tên Khách hàng";
                tenKHColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tenKHColumn.ReadOnly = true;
                tenKHColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvPotentialCustomers.Columns.Add(tenKHColumn);

                DataGridViewTextBoxColumn sdtColumn = new DataGridViewTextBoxColumn();
                sdtColumn.DataPropertyName = "Sodienthoai";
                sdtColumn.Name = "Sodienthoa";
                sdtColumn.HeaderText = "Số điện thoại";
                sdtColumn.Width = 120;
                sdtColumn.ReadOnly = true;
                sdtColumn.Resizable = DataGridViewTriState.False;
                sdtColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvPotentialCustomers.Columns.Add(sdtColumn);

                DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn();
                emailColumn.DataPropertyName = "Email";
                emailColumn.Name = "Email";
                emailColumn.HeaderText = "Email";
                emailColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                emailColumn.ReadOnly = true;
                emailColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvPotentialCustomers.Columns.Add(emailColumn);

                DataGridViewTextBoxColumn ngayDangKyColumn = new DataGridViewTextBoxColumn();
                ngayDangKyColumn.DataPropertyName = "Ngaydangky";
                ngayDangKyColumn.Name = "Ngaydangky";
                ngayDangKyColumn.HeaderText = "Ngày đăng ký";
                ngayDangKyColumn.Width = 150;
                ngayDangKyColumn.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                ngayDangKyColumn.ReadOnly = true;
                ngayDangKyColumn.Resizable = DataGridViewTriState.False;
                ngayDangKyColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvPotentialCustomers.Columns.Add(ngayDangKyColumn);

                DataGridViewTextBoxColumn diemTichLuyColumn = new DataGridViewTextBoxColumn();
                diemTichLuyColumn.DataPropertyName = "DiemTichLuy";
                diemTichLuyColumn.Name = "DiemTichLuy";
                diemTichLuyColumn.HeaderText = "Điểm Tích Lũy";
                diemTichLuyColumn.Width = 100;
                diemTichLuyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                diemTichLuyColumn.ReadOnly = true;
                diemTichLuyColumn.Resizable = DataGridViewTriState.False;
                diemTichLuyColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvPotentialCustomers.Columns.Add(diemTichLuyColumn);


                // 4. Gán dữ liệu cho DataSource sau khi các cột đã được định nghĩa
                // KHÔNG CẦN DgvPotentialCustomers.Rows.Clear() trước khi gán DataSource
                // Và KHÔNG CẦN thêm hàng thủ công sau đó.
                dgvPotentialCustomers.DataSource = potentialCustomers;

                // 5. Điền STT bằng sự kiện CellFormatting
                // Đảm bảo bạn đã đăng ký sự kiện này trong constructor hoặc trong Designer
                // dgvPotentialCustomers.CellFormatting += dgvPotentialCustomers_CellFormatting;

                if (potentialCustomers.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào có điểm tích lũy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo TOP khách hàng tiềm năng: {ex.Message}\nVui lòng kiểm tra kết nối CSDL và dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm sự kiện CellFormatting vào Form của bạn
        private void dgvPotentialCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPotentialCustomers.Columns[e.ColumnIndex].Name == "STT" && e.RowIndex >= 0)
            {
                e.Value = e.RowIndex + 1;
                e.FormattingApplied = true; // Đánh dấu là đã xử lý định dạng
            }
        }
        private void LoadRevenueReport()
        {
            try
            {
                DateTime startDate = dtpRevenueStartDate.Value.Date;
                DateTime endDate = dtpRevenueEndDate.Value.Date;

                // Kiểm tra lỗi ngày trước khi gọi BLL
                if (startDate > endDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Lỗi Ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblTotalPrice.Text = "0 VNĐ"; // Reset tổng tiền
                    return; // Thoát khỏi phương thức
                }

                // Gọi BLL để lấy dữ liệu, BLL sẽ ném exception nếu ngày không hợp lệ
                List<RevenueReportItem> revenueData = _reportBLL.GetRevenueReport(startDate, endDate);

                // Quan trọng: Ngắt kết nối nguồn dữ liệu cũ và xóa cột cũ
                dgvRevenue.DataSource = null;
                dgvRevenue.Columns.Clear(); // Xóa tất cả các cột cũ

                // Đảm bảo AutoGenerateColumns là false
                dgvRevenue.AutoGenerateColumns = false;

                // Cấu hình các cột (nếu chưa cấu hình trong Designer)
                // Cột "No" (Số thứ tự) - KHÔNG GÁN DataPropertyName
                DataGridViewTextBoxColumn noColumn = new DataGridViewTextBoxColumn();
                noColumn.Name = "No";
                noColumn.HeaderText = "No";
                noColumn.Width = 100;
                noColumn.ReadOnly = true;
                noColumn.Resizable = DataGridViewTriState.False;
                noColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRevenue.Columns.Add(noColumn);

                // Cột "Date" (Ngày)
                DataGridViewTextBoxColumn dateColumn = new DataGridViewTextBoxColumn();
                dateColumn.DataPropertyName = "Ngay"; // DGV sẽ tự lấy giá trị từ đây
                dateColumn.Name = "Date";
                dateColumn.HeaderText = "Date";
                dateColumn.Width = 200;
                dateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
                dateColumn.ReadOnly = true;
                dateColumn.Resizable = DataGridViewTriState.False;
                dateColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRevenue.Columns.Add(dateColumn);

                // Cột "Price" (Giá)
                DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();
                priceColumn.DataPropertyName = "Tongtien"; // DGV sẽ tự lấy giá trị từ đây
                priceColumn.Name = "Price";
                priceColumn.HeaderText = "Price";
                priceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                priceColumn.DefaultCellStyle.Format = "N0";
                priceColumn.ReadOnly = true;
                priceColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                priceColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRevenue.Columns.Add(priceColumn);

                // Gán dữ liệu vào DataSource sau khi các cột đã được định nghĩa
                // KHÔNG CẦN dgvRevenue.Rows.Clear() trước khi gán DataSource
                // Và KHÔNG CẦN thêm hàng thủ công bằng Rows.Add() và gán Cell.Value
                dgvRevenue.DataSource = revenueData;

                // Điền STT bằng sự kiện CellFormatting
                // Đảm bảo bạn đã đăng ký sự kiện này trong constructor hoặc trong Designer
                // dgvRevenue.CellFormatting += dgvRevenue_CellFormatting;

                // Tính tổng doanh thu và hiển thị lên lblTotalPrice
                decimal totalRevenue = revenueData.Sum(item => item.Tongtien);
                lblTotalPrice.Text = totalRevenue.ToString("N0") + " VNĐ";

                if (revenueData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu doanh thu trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo doanh thu: {ex.Message}\nVui lòng kiểm tra kết nối CSDL và dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm sự kiện CellFormatting vào Form của bạn
        private void dgvRevenue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRevenue.Columns[e.ColumnIndex].Name == "No" && e.RowIndex >= 0)
            {
                e.Value = e.RowIndex + 1;
                e.FormattingApplied = true; // Đánh dấu là đã xử lý định dạng
            }
        }

        /// <summary>
        /// Tải dữ liệu báo cáo bán hàng theo đồ uống và hiển thị lên DataGridView.
        /// </summary>
        private void LoadProductSalesReport()
        {
            try
            {
                // Giả sử bạn có dtpProductSalesStartDate và dtpProductSalesEndDate trong Designer
                // Nếu không có, bạn có thể sử dụng DateTime.Now hoặc một khoảng thời gian mặc định.
                DateTime startDate = DateTime.Now.AddMonths(-1).Date; // Ví dụ: 1 tháng trước
                DateTime endDate = DateTime.Now.Date; // Ví dụ: Hôm nay
                dgvProductSales.DataSource = null; // Ngắt kết nối nguồn dữ liệu
                dgvProductSales.Rows.Clear();     // Xóa tất cả các hàng
                dgvProductSales.Columns.Clear();  // Xóa tất cả các cột

                // Kiểm tra lỗi ngày trước khi gọi BLL
                if (startDate > endDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Lỗi Ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblTong.Text = "0 VNĐ";
                    return; // Thoát khỏi phương thức
                }

                // Gọi BLL để lấy dữ liệu báo cáo bán hàng theo đồ uống
                List<ProductSalesReportItem> productSalesData = _reportBLL.GetProductSalesReport(startDate, endDate);

                // Tính tổng doanh thu toàn bộ để tính tỷ lệ đóng góp
                decimal overallTotalRevenue = productSalesData.Sum(item => item.TongDoanhThuMon);

                // Quan trọng: Đảm bảo AutoGenerateColumns là false
                dgvProductSales.AutoGenerateColumns = false; // Giả sử tên DGV là dgvProductSales

                // Xóa tất cả các hàng và cột hiện có để đảm bảo cấu trúc mới
                dgvProductSales.Rows.Clear();
                dgvProductSales.Columns.Clear();

                // Thêm và cấu hình các cột
                // Cột "STT"
                DataGridViewTextBoxColumn sttColumn = new DataGridViewTextBoxColumn();
                sttColumn.Name = "STT";
                sttColumn.HeaderText = "STT";
                sttColumn.Width = 50;
                sttColumn.ReadOnly = true;
                sttColumn.Resizable = DataGridViewTriState.False;
                sttColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvProductSales.Columns.Add(sttColumn);

                // Cột "Madouong"
                DataGridViewTextBoxColumn maDouongColumn = new DataGridViewTextBoxColumn();
                maDouongColumn.DataPropertyName = "Madouong";
                maDouongColumn.Name = "Madouong";
                maDouongColumn.HeaderText = "Mã Đồ uống";
                maDouongColumn.Width = 100;
                maDouongColumn.ReadOnly = true;
                maDouongColumn.Resizable = DataGridViewTriState.False;
                maDouongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvProductSales.Columns.Add(maDouongColumn);

                // Cột "Tendouong" - Tự động lấp đầy
                DataGridViewTextBoxColumn tenDouongColumn = new DataGridViewTextBoxColumn();
                tenDouongColumn.DataPropertyName = "Tendouong";
                tenDouongColumn.Name = "Tendouong";
                tenDouongColumn.HeaderText = "Tên Đồ uống";
                tenDouongColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                tenDouongColumn.ReadOnly = true;
                tenDouongColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvProductSales.Columns.Add(tenDouongColumn);

                // Cột "Maloai"
                DataGridViewTextBoxColumn maLoaiColumn = new DataGridViewTextBoxColumn();
                maLoaiColumn.DataPropertyName = "Maloai";
                maLoaiColumn.Name = "Maloai";
                maLoaiColumn.HeaderText = "Loại Đồ uống";
                maLoaiColumn.Width = 120;
                maLoaiColumn.ReadOnly = true;
                maLoaiColumn.Resizable = DataGridViewTriState.False;
                maLoaiColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvProductSales.Columns.Add(maLoaiColumn);

                // Cột "SoLuongBan"
                DataGridViewTextBoxColumn soLuongBanColumn = new DataGridViewTextBoxColumn();
                soLuongBanColumn.DataPropertyName = "SoLuongBan";
                soLuongBanColumn.Name = "SoLuongBan";
                soLuongBanColumn.HeaderText = "Số lượng bán";
                soLuongBanColumn.Width = 100;
                soLuongBanColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                soLuongBanColumn.ReadOnly = true;
                soLuongBanColumn.Resizable = DataGridViewTriState.False;
                soLuongBanColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvProductSales.Columns.Add(soLuongBanColumn);

                // Cột "TongDoanhThuMon"
                DataGridViewTextBoxColumn tongDoanhThuMonColumn = new DataGridViewTextBoxColumn();
                tongDoanhThuMonColumn.DataPropertyName = "TongDoanhThuMon";
                tongDoanhThuMonColumn.Name = "TongDoanhThuMon";
                tongDoanhThuMonColumn.HeaderText = "Tổng Doanh thu";
                tongDoanhThuMonColumn.Width = 150;
                tongDoanhThuMonColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                tongDoanhThuMonColumn.DefaultCellStyle.Format = "N0";
                tongDoanhThuMonColumn.ReadOnly = true;
                tongDoanhThuMonColumn.Resizable = DataGridViewTriState.False;
                tongDoanhThuMonColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvProductSales.Columns.Add(tongDoanhThuMonColumn);

                // Cột "TyLeDongGopDoanhThu" (Tính toán ở đây, không phải DataPropertyName trực tiếp từ ProductSalesReportItem)
                DataGridViewTextBoxColumn tyLeDongGopColumn = new DataGridViewTextBoxColumn();
                tyLeDongGopColumn.Name = "TyLeDongGopDoanhThu";
                tyLeDongGopColumn.HeaderText = "Tỷ lệ đóng góp";
                tyLeDongGopColumn.Width = 120;
                tyLeDongGopColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                tyLeDongGopColumn.DefaultCellStyle.Format = "P2"; // Định dạng phần trăm với 2 chữ số thập phân
                tyLeDongGopColumn.ReadOnly = true;
                tyLeDongGopColumn.Resizable = DataGridViewTriState.False;
                tyLeDongGopColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvProductSales.Columns.Add(tyLeDongGopColumn);


                // Gán dữ liệu vào DataGridView
                dgvProductSales.DataSource = productSalesData;

                // Điền STT và tính toán Tỷ lệ đóng góp doanh thu
                for (int i = 0; i < dgvProductSales.Rows.Count; i++)
                {
                    ProductSalesReportItem item = productSalesData[i];
                    dgvProductSales.Rows[i].Cells["STT"].Value = i + 1;

                    // Tính toán và gán tỷ lệ đóng góp
                    if (overallTotalRevenue > 0)
                    {
                        // Giả sử ProductSalesReportItem đã có thuộc tính TyLeDongGopDoanhThu
                        // Bạn sẽ cần cập nhật thuộc tính này TRONG BLL hoặc tính toán trực tiếp ở đây.
                        // Để đơn giản, tôi sẽ tính toán ở đây và gán vào cột hiển thị.
                        dgvProductSales.Rows[i].Cells["TyLeDongGopDoanhThu"].Value = (double)item.TongDoanhThuMon / (double)overallTotalRevenue;
                    }
                    else
                    {
                        dgvProductSales.Rows[i].Cells["TyLeDongGopDoanhThu"].Value = 0d;
                    }
                }
                lblTong.Text = overallTotalRevenue.ToString("N0") + " VNĐ";
                if (productSalesData.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu bán hàng theo đồ uống trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex) // Bắt lỗi từ BLL nếu ngày không hợp lệ
            {
                MessageBox.Show(ex.Message, "Lỗi Ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo bán hàng theo đồ uống: {ex.Message}\nVui lòng kiểm tra kết nối CSDL và dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintDoanhThu_Click(object sender, EventArgs e)
        {
        }


        private void btnPrintBestseller_Click(object sender, EventArgs e)
        {
        }

    }
}