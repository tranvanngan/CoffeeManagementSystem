using CoffeeManagementSystem.DAL;
using CoffeeManagementSystem.BLL; // Thêm dòng này để sử dụng BLL
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq; // Cần thiết nếu bạn dùng LINQ (cho Sum())
using System.Drawing;
using System.Drawing.Printing;
namespace CoffeeManagementSystem
{
    public partial class ReportForm : Form
    {
        private ReportBLL _reportBLL; // Khai báo đối tượng BLL thay vì các DAL riêng lẻ
                                      // Đối tượng để điều khiển việc in ấn
        private PrintDocument printDocumentPotentialCustomers;
        private PrintDocument printDocumentRevenue;
        private PrintDocument printDocumentProductSales;

        // Các đối tượng PrintPreviewDialog để xem trước khi in
        private PrintPreviewDialog printPreviewDialogPotentialCustomers;
        private PrintPreviewDialog printPreviewDialogRevenue;
        private PrintPreviewDialog printPreviewDialogProductSales;

        // Biến để lưu trữ DataGridView hiện tại đang được in
        private DataGridView dgvToPrint;
        // Biến để theo dõi số trang hiện tại khi in
        private int currentRowIndex = 0;
        private string reportTitle = ""; // Tiêu đề báo cáo
        private string reportDateRange = ""; // Khoảng thời gian báo cáo
        public ReportForm()
        {
            InitializeComponent();

            _reportBLL = new ReportBLL(); // Khởi tạo BLL
                                          // Khởi tạo PrintDocument và PrintPreviewDialog cho báo cáo Khách hàng tiềm năng
            printDocumentPotentialCustomers = new PrintDocument();
            printDocumentPotentialCustomers.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
            printPreviewDialogPotentialCustomers = new PrintPreviewDialog();
            printPreviewDialogPotentialCustomers.Document = printDocumentPotentialCustomers;

            // Khởi tạo PrintDocument và PrintPreviewDialog cho báo cáo Doanh thu
            printDocumentRevenue = new PrintDocument();
            printDocumentRevenue.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
            printPreviewDialogRevenue = new PrintPreviewDialog();
            printPreviewDialogRevenue.Document = printDocumentRevenue;

            // Khởi tạo PrintDocument và PrintPreviewDialog cho báo cáo Bán hàng theo đồ uống
            printDocumentProductSales = new PrintDocument();
            printDocumentProductSales.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
            printPreviewDialogProductSales = new PrintPreviewDialog();
            printPreviewDialogProductSales.Document = printDocumentProductSales;


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
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Arial", 10);
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 12, FontStyle.Bold);
            float lineHeight = font.GetHeight();
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float colWidth = 0;

            // In tiêu đề báo cáo
            StringFormat sfCenter = new StringFormat();
            sfCenter.Alignment = StringAlignment.Center;
            sfCenter.LineAlignment = StringAlignment.Center;
            graphics.DrawString(reportTitle, headerFont, Brushes.Black, e.PageBounds.Width / 2, y, sfCenter);
            y += headerFont.GetHeight() + 10; // Khoảng cách sau tiêu đề

            // In khoảng thời gian (nếu có)
            if (!string.IsNullOrEmpty(reportDateRange))
            {
                graphics.DrawString(reportDateRange, subHeaderFont, Brushes.Black, e.PageBounds.Width / 2, y, sfCenter);
                y += subHeaderFont.GetHeight() + 20; // Khoảng cách sau khoảng thời gian
            }
            else
            {
                y += 20; // Khoảng cách nếu không có khoảng thời gian
            }


            // Tính toán chiều rộng cột dựa trên DataGridView đang in
            // Cần phải tính lại vì DataGridView có thể thay đổi số lượng và độ rộng cột
            List<float> columnWidths = new List<float>();
            float totalPrintableWidth = e.MarginBounds.Width;
            float totalProportionalWidth = 0;

            foreach (DataGridViewColumn col in dgvToPrint.Columns)
            {
                if (col.Visible) // Chỉ in các cột hiển thị
                {
                    // Nếu cột có Fill, xử lý riêng sau
                    if (col.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
                    {
                        // Tạm thời coi là 1 đơn vị tỉ lệ, sẽ chia lại sau
                        totalProportionalWidth += 1;
                        columnWidths.Add(0); // Đặt chỗ cho cột Fill
                    }
                    else
                    {
                        // Sử dụng chiều rộng thực tế của cột trong DataGridView
                        columnWidths.Add(col.Width);
                        totalPrintableWidth -= col.Width;
                    }
                }
            }

            // Phân bổ chiều rộng cho các cột Fill
            for (int i = 0; i < dgvToPrint.Columns.Count; i++)
            {
                DataGridViewColumn col = dgvToPrint.Columns[i];
                if (col.Visible && col.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
                {
                    if (totalProportionalWidth > 0)
                    {
                        columnWidths[i] = totalPrintableWidth / totalProportionalWidth;
                    }
                    else
                    {
                        columnWidths[i] = col.Width; // Fallback nếu không có cột nào là Fill
                    }
                }
            }


            // In tiêu đề cột
            float currentX = x;
            for (int i = 0; i < dgvToPrint.Columns.Count; i++)
            {
                DataGridViewColumn col = dgvToPrint.Columns[i];
                if (col.Visible)
                {
                    colWidth = columnWidths[i];
                    graphics.DrawString(col.HeaderText, subHeaderFont, Brushes.Black, currentX, y);
                    currentX += colWidth;
                }
            }
            y += subHeaderFont.GetHeight() + 5; // Khoảng cách sau tiêu đề cột
            graphics.DrawLine(Pens.Black, e.MarginBounds.Left, y, e.MarginBounds.Right, y); // Đường kẻ dưới tiêu đề
            y += 5; // Khoảng cách sau đường kẻ

            // In dữ liệu
            while (currentRowIndex < dgvToPrint.Rows.Count)
            {
                DataGridViewRow row = dgvToPrint.Rows[currentRowIndex];
                if (row.Visible) // Chỉ in các hàng hiển thị
                {
                    if (y + lineHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true; // Còn dữ liệu, chuyển sang trang mới
                        currentRowIndex++; // Chuẩn bị cho hàng đầu tiên của trang tiếp theo
                        return; // Dừng việc vẽ trên trang hiện tại
                    }

                    currentX = x;
                    for (int i = 0; i < dgvToPrint.Columns.Count; i++)
                    {
                        DataGridViewColumn col = dgvToPrint.Columns[i];
                        if (col.Visible)
                        {
                            colWidth = columnWidths[i];
                            object cellValue = row.Cells[col.Name].Value; // Lấy giá trị dựa trên tên cột

                            string text = (cellValue == null) ? "" : cellValue.ToString();

                            // Áp dụng định dạng cho các cột số/ngày nếu cần
                            if (col.DefaultCellStyle.Format != null && cellValue is IFormattable)
                            {
                                text = ((IFormattable)cellValue).ToString(col.DefaultCellStyle.Format, System.Globalization.CultureInfo.CurrentCulture);
                            }

                            // Căn lề cho cột
                            StringFormat cellSf = new StringFormat(StringFormatFlags.NoWrap);
                            if (col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight || col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.TopRight || col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.BottomRight)
                            {
                                cellSf.Alignment = StringAlignment.Far;
                            }
                            else if (col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter || col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.TopCenter || col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.BottomCenter)
                            {
                                cellSf.Alignment = StringAlignment.Center;
                            }
                            else
                            {
                                cellSf.Alignment = StringAlignment.Near;
                            }

                            // Vẽ chuỗi
                            graphics.DrawString(text, font, Brushes.Black, new RectangleF(currentX, y, colWidth, lineHeight), cellSf);
                            currentX += colWidth;
                        }
                    }
                    y += lineHeight;
                }
                currentRowIndex++;
            }

            e.HasMorePages = false; // Đã in hết dữ liệu
            currentRowIndex = 0; // Reset lại cho lần in tiếp theo
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
            // Báo cáo Khách hàng tiềm năng
            dgvToPrint = dgvPotentialCustomers; // Gán DataGridView cần in
            reportTitle = "BÁO CÁO TOP 10 KHÁCH HÀNG TIỀM NĂNG";
            reportDateRange = ""; // Không có khoảng thời gian cho báo cáo này
            currentRowIndex = 0; // Reset số trang
            printPreviewDialogPotentialCustomers.ShowDialog();
        }

        private void btnPrintDoanhThu_Click(object sender, EventArgs e)
        {
            // Báo cáo Doanh thu
            dgvToPrint = dgvRevenue; // Gán DataGridView cần in
            reportTitle = "BÁO CÁO DOANH THU";
            reportDateRange = $"Từ ngày: {dtpRevenueStartDate.Value.ToShortDateString()} đến ngày: {dtpRevenueEndDate.Value.ToShortDateString()}";
            currentRowIndex = 0; // Reset số trang
            printPreviewDialogRevenue.ShowDialog();
        }

        private void btnPrintBestseller_Click(object sender, EventArgs e)
        {
            // Báo cáo Bán hàng theo đồ uống
            dgvToPrint = dgvProductSales; // Gán DataGridView cần in
            reportTitle = "BÁO CÁO SẢN PHẨM BÁN CHẠY NHẤT";
            reportDateRange = $"Từ ngày: {dtpProductSalesStartDate.Value.ToShortDateString()} đến ngày: {dtpProductSalesEndDate.Value.ToShortDateString()}";
            currentRowIndex = 0; // Reset số trang
            printPreviewDialogProductSales.ShowDialog();
        }

    }
}