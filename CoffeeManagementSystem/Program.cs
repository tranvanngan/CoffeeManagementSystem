using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DangNhapForm());
        }
    }
}
