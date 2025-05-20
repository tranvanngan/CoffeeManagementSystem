using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms; // Chỉ dùng cho MessageBox ví dụ
using System.Data; // Cần cho DBNull.Value

// Đảm bảo using namespace chứa lớp BaseDataAccess
// using YourNamespace.DAL;

// Đảm bảo using namespace chứa lớp Model Khachhang
// using YourNamespace.Models; // Hoặc namespace thực tế của bạn
using CoffeeManagementSystem;
namespace CoffeeManagementSystem.DAL
{
    public class KhachhangDAL : BaseDataAccess // Kế thừa từ lớp BaseDataAccess
    {
        public KhachhangDAL() : base() // Gọi constructor của lớp base để lấy ConnectionString
        {
        }

        // =====================================================
        // PHƯƠNG THỨC LẤY DANH SÁCH KHÁCH HÀNG
        // =====================================================

        /// <summary>
        /// Lấy tất cả khách hàng từ CSDL.
        /// </summary>
        /// <returns>Danh sách các đối tượng Khachhang.</returns>
        public List<Khachhang> GetAllKhachhangs()
        {
            List<Khachhang> khachhangs = new List<Khachhang>();

            // Sử dụng ConnectionString từ lớp BaseDataAccess
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    string selectSql = "SELECT Makhachhang, Hoten, Sodienthoai, Email, Ngaydangky, Diemtichluy FROM Khachhang";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader()) // Thực thi SELECT và lấy DataReader
                        {
                            while (reader.Read()) // Đọc từng dòng dữ liệu
                            {
                                Khachhang khachhang = new Khachhang
                                {
                                    // Đọc dữ liệu từ reader và ánh xạ vào thuộc tính của object Khachhang
                                    Makhachhang = reader["Makhachhang"].ToString(),
                                    Hoten = reader["Hoten"].ToString(),
                                    // Kiểm tra DBNull cho các cột có thể NULL
                                    Sodienthoai = reader["Sodienthoai"] != DBNull.Value ? reader["Sodienthoai"].ToString() : null,
                                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                    // Đọc TEXT và chuyển đổi sang DateTime
                                    Ngaydangky = DateTime.Parse(reader["Ngaydangky"].ToString()),
                                    // Đọc INTEGER và chuyển đổi sang int
                                    Diemtichluy = Convert.ToInt32(reader["Diemtichluy"])
                                };
                                khachhangs.Add(khachhang); // Thêm object vào danh sách
                            }
                        } // DataReader tự đóng khi thoát using
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (ví dụ: hiển thị MessageBox hoặc ghi log)
                    MessageBox.Show($"Lỗi khi lấy danh sách khách hàng: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Tùy chọn: ném lại exception để lớp gọi xử lý
                    // throw;
                }
            } // Connection tự đóng khi thoát using
            return khachhangs; // Trả về danh sách khách hàng
        }

        // =====================================================
        // PHƯƠNG THỨC LẤY KHÁCH HÀNG THEO ID
        // =====================================================

        /// <summary>
        /// Lấy thông tin khách hàng theo Mã khách hàng.
        /// </summary>
        /// <param name="makhachhang">Mã khách hàng cần lấy.</param>
        /// <returns>Đối tượng Khachhang nếu tìm thấy, ngược lại trả về null.</returns>
        public Khachhang GetKhachhangById(string makhachhang)
        {
            Khachhang khachhang = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Makhachhang, Hoten, Sodienthoai, Email, Ngaydangky, Diemtichluy FROM Khachhang WHERE Makhachhang = @Makhachhang";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        // Sử dụng Parameter cho điều kiện WHERE
                        command.Parameters.AddWithValue("@Makhachhang", makhachhang);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Đọc hàng đầu tiên (và duy nhất) nếu có
                            {
                                khachhang = new Khachhang
                                {
                                    Makhachhang = reader["Makhachhang"].ToString(),
                                    Hoten = reader["Hoten"].ToString(),
                                    Sodienthoai = reader["Sodienthoai"] != DBNull.Value ? reader["Sodienthoai"].ToString() : null,
                                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                    Ngaydangky = DateTime.Parse(reader["Ngaydangky"].ToString()),
                                    Diemtichluy = Convert.ToInt32(reader["Diemtichluy"])
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy khách hàng theo ID: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
            return khachhang; // Trả về object Khachhang hoặc null
        }

        // =====================================================
        // PHƯƠNG THỨC THÊM KHÁCH HÀNG MỚI
        // =====================================================

        /// <summary>
        /// Thêm một khách hàng mới vào CSDL.
        /// </summary>
        /// <param name="khachhang">Đối tượng Khachhang cần thêm.</param>
        public void AddKhachhang(Khachhang khachhang)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = @"
                    INSERT INTO Khachhang (Makhachhang, Hoten, Sodienthoai, Email, Ngaydangky, Diemtichluy)
                    VALUES (@Makhachhang, @Hoten, @Sodienthoai, @Email, @Ngaydangky, @Diemtichluy)";

                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        // Sử dụng Parameters để chèn dữ liệu
                        command.Parameters.AddWithValue("@Makhachhang", khachhang.Makhachhang);
                        command.Parameters.AddWithValue("@Hoten", khachhang.Hoten);
                        // Xử lý cột có thể NULL: nếu giá trị C# là null, chèn DBNull.Value
                        command.Parameters.AddWithValue("@Sodienthoai", (object)khachhang.Sodienthoai ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)khachhang.Email ?? DBNull.Value);
                        // Chuyển DateTime sang TEXT để lưu
                        command.Parameters.AddWithValue("@Ngaydangky", khachhang.Ngaydangky.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Diemtichluy", khachhang.Diemtichluy); // int -> INTEGER

                        command.ExecuteNonQuery(); // Thực thi lệnh INSERT
                                                   // MessageBox.Show("Đã thêm khách hàng thành công.", "Thông báo"); // Tùy chọn
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm khách hàng: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
        }

        // =====================================================
        // PHƯƠNG THỨC CẬP NHẬT THÔNG TIN KHÁCH HÀNG
        // =====================================================

        /// <summary>
        /// Cập nhật thông tin của một khách hàng.
        /// </summary>
        /// <param name="khachhang">Đối tượng Khachhang chứa thông tin cập nhật (cần có Makhachhang).</param>
        public void UpdateKhachhang(Khachhang khachhang)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string updateSql = @"
                    UPDATE Khachhang
                    SET Hoten = @Hoten,
                        Sodienthoai = @Sodienthoai,
                        Email = @Email,
                        Ngaydangky = @Ngaydangky,
                        Diemtichluy = @Diemtichluy
                    WHERE Makhachhang = @Makhachhang"; // Cập nhật dựa trên khóa chính

                    using (SQLiteCommand command = new SQLiteCommand(updateSql, connection))
                    {
                        // Sử dụng Parameters để cập nhật dữ liệu
                        command.Parameters.AddWithValue("@Hoten", khachhang.Hoten);
                        command.Parameters.AddWithValue("@Sodienthoai", (object)khachhang.Sodienthoai ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)khachhang.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Ngaydangky", khachhang.Ngaydangky.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Diemtichluy", khachhang.Diemtichluy);
                        // Parameter cho điều kiện WHERE
                        command.Parameters.AddWithValue("@Makhachhang", khachhang.Makhachhang);

                        int rowsAffected = command.ExecuteNonQuery(); // Thực thi lệnh UPDATE
                                                                      // MessageBox.Show($"Đã cập nhật {rowsAffected} khách hàng.", "Thông báo"); // Tùy chọn
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật khách hàng: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
        }

        // =====================================================
        // PHƯƠNG THỨC XÓA KHÁCH HÀNG
        // =====================================================

        /// <summary>
        /// Xóa một khách hàng khỏi CSDL.
        /// </summary>
        /// <param name="makhachhang">Mã khách hàng cần xóa.</param>
        public void DeleteKhachhang(string makhachhang)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string deleteSql = "DELETE FROM Khachhang WHERE Makhachhang = @Makhachhang";

                    using (SQLiteCommand command = new SQLiteCommand(deleteSql, connection))
                    {
                        // Sử dụng Parameter cho điều kiện WHERE
                        command.Parameters.AddWithValue("@Makhachhang", makhachhang);

                        int rowsAffected = command.ExecuteNonQuery(); // Thực thi lệnh DELETE
                                                                      // MessageBox.Show($"Đã xóa {rowsAffected} khách hàng.", "Thông báo"); // Tùy chọn
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa khách hàng: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
        }

        // =====================================================
        // PHƯƠNG THỨC TÌM KIẾM KHÁCH HÀNG
        // =====================================================

        /// <summary>
        /// Tìm kiếm khách hàng dựa trên từ khóa trong các cột Makhachhang, Hoten, Sodienthoai, Email.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm.</param>
        /// <returns>Danh sách các đối tượng Khachhang phù hợp.</returns>
        public List<Khachhang> SearchKhachhangs(string searchTerm)
        {
            List<Khachhang> khachhangs = new List<Khachhang>();

            // Kiểm tra nếu từ khóa rỗng hoặc null, trả về danh sách rỗng
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return khachhangs;
            }

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    // Câu lệnh SELECT với điều kiện WHERE sử dụng LIKE để tìm kiếm theo mẫu
                    // Tìm kiếm trong Makhachhang, Hoten, Sodienthoai, Email
                    // Sử dụng LOWER() để tìm kiếm không phân biệt chữ hoa/thường
                    string selectSql = @"
                    SELECT Makhachhang, Hoten, Sodienthoai, Email, Ngaydangky, Diemtichluy
                    FROM Khachhang
                    WHERE LOWER(Makhachhang) LIKE @SearchTerm
                       OR LOWER(Hoten) LIKE @SearchTerm
                       OR LOWER(Sodienthoai) LIKE @SearchTerm
                       OR LOWER(Email) LIKE @SearchTerm";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        // Sử dụng Parameter với ký tự wildcard %
                        // % ở đầu và cuối cho phép tìm kiếm bất kỳ chuỗi nào chứa searchTerm
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm.ToLower() + "%");

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Khachhang khachhang = new Khachhang
                                {
                                    Makhachhang = reader["Makhachhang"].ToString(),
                                    Hoten = reader["Hoten"].ToString(),
                                    Sodienthoai = reader["Sodienthoai"] != DBNull.Value ? reader["Sodienthoai"].ToString() : null,
                                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                    Ngaydangky = DateTime.Parse(reader["Ngaydangky"].ToString()),
                                    Diemtichluy = Convert.ToInt32(reader["Diemtichluy"])
                                };
                                khachhangs.Add(khachhang);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm khách hàng: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
            return khachhangs; // Trả về danh sách khách hàng phù hợp
        }
    }
}