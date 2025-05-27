using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace CoffeeManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Tạo đối tượng khởi tạo CSDL với tên file mong muốn
            // DatabaseInitializer dbInitializer = new DatabaseInitializer("QuanLyCaPheDatabase"); // Thay QuanLyCaPheDatabase bằng tên bạn muốn cho file .db

            // Gọi hàm khởi tạo CSDL
            //dbInitializer.InitializeDatabase();
            // --- KIỂM TRA KẾT NỐI CSDL ---
            string connectionString = ConfigurationManager.ConnectionStrings["SqliteDbConnection"].ConnectionString; // Lấy chuỗi kết nối từ App.config

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Lỗi: Không tìm thấy chuỗi kết nối 'SqliteDbConnection' trong App.config.", "Lỗi cấu hình CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Tùy chọn: Thoát ứng dụng nếu không có chuỗi kết nối
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Thử mở kết nối
                                       // Nếu đến được đây mà không có lỗi, kết nối thành công

                    MessageBox.Show("Kết nối CSDL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng kết nối ngay sau khi kiểm tra
                    // Khối 'using' sẽ tự động gọi connection.Dispose() và đóng kết nối

                }
                catch (Exception ex)
                {
                    // Nếu có lỗi khi mở kết nối
                    MessageBox.Show($"Lỗi kết nối CSDL: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Tùy chọn: Thoát ứng dụng nếu không kết nối được CSDL
                    // return;
                }
            }
            InitializeDatabase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DangNhapForm());
        }
        private static void InitializeDatabase()
        {
            string dbFileName = "QuanLyCaPheDatabase.db";

            // Đường dẫn đến thư mục mà file .exe của bạn sẽ chạy (ví dụ: bin\Debug)
            string appRunDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string destinationPath = Path.Combine(appRunDirectory, dbFileName);

            // Đường dẫn đến file DB gốc của bạn trong thư mục Data của dự án
            // Dựa trên cấu trúc dự án của bạn (CoffeeManagementSystem/CoffeeManagementSystem/Data/QuanLyCaPheDatabase.db)
            // Khi bạn Build, thư mục Data sẽ được sao chép vào bin\Debug
            string sourcePath = Path.Combine(appRunDirectory, "Data", dbFileName);

            // Kiểm tra xem file DB đã tồn tại ở nơi ứng dụng sẽ tìm thấy chưa (tức là trong bin\Debug)
            if (!File.Exists(destinationPath))
            {
                try
                {
                    // Nếu chưa tồn tại, sao chép từ thư mục Data (nơi bản gốc) vào thư mục chạy ứng dụng
                    File.Copy(sourcePath, destinationPath, true); // true để ghi đè nếu file đích tồn tại (đề phòng)
                    MessageBox.Show("Cơ sở dữ liệu đã được sao chép thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sao chép cơ sở dữ liệu: {ex.Message}\nHãy đảm bảo ứng dụng có quyền truy cập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Rất quan trọng: Bạn có thể muốn thoát ứng dụng nếu không thể sao chép DB.
                    Application.Exit(); // Thoát ứng dụng nếu không có DB để làm việc
                }
            }
        }
    }
}

