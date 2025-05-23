using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms; // Chỉ dùng cho MessageBox trong ví dụ xử lý lỗi
using System.Data; // Cần cho DBNull.Value

// Đảm bảo using namespace chứa lớp BaseDataAccess của bạn
// Ví dụ: using CoffeeManagementSystem.DAL;
// Nếu BaseDataAccess nằm trực tiếp trong CoffeeManagementSystem, bạn không cần using riêng
// Giả định BaseDataAccess nằm trong cùng namespace hoặc đã được using ở nơi khác.

// Đảm bảo using namespace chứa lớp Model Nhanvien của bạn
using CoffeeManagementSystem; // Dựa trên namespace của lớp Nhanvien bạn cung cấp

namespace CoffeeManagementSystem.DAL // Đặt DAL trong một namespace con để tổ chức code tốt hơn
{
    public class NhanvienDAL : BaseDataAccess // Kế thừa từ lớp BaseDataAccess
    {
        public NhanvienDAL() : base() // Gọi constructor của lớp base để lấy ConnectionString
        {
        }

        // =====================================================
        // PHƯƠNG THỨC LẤY DANH SÁCH NHÂN VIÊN
        // =====================================================

        /// <summary>
        /// Lấy tất cả nhân viên từ CSDL.
        /// </summary>
        /// <returns>Danh sách các đối tượng Nhanvien.</returns>
        public List<Nhanvien> GetAllNhanviens()
        {
            List<Nhanvien> nhanviens = new List<Nhanvien>();

            // Sử dụng ConnectionString từ lớp BaseDataAccess
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open(); // Mở kết nối

                    string selectSql = "SELECT Manhanvien, Hoten, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Email, Ngayvaolam FROM Nhanvien";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader()) // Thực thi SELECT và lấy DataReader
                        {
                            while (reader.Read()) // Đọc từng dòng dữ liệu
                            {
                                Nhanvien nhanvien = new Nhanvien
                                {
                                    // Đọc dữ liệu từ reader và ánh xạ vào thuộc tính của object Nhanvien
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Hoten = reader["Hoten"].ToString(),
                                    // Đọc TEXT và chuyển đổi sang DateTime
                                    Ngaysinh = DateTime.Parse(reader["Ngaysinh"].ToString()),
                                    Gioitinh = reader["Gioitinh"].ToString(),
                                    Diachi = reader["Diachi"].ToString(),
                                    // Kiểm tra DBNull cho các cột có thể NULL
                                    Sodienthoai = reader["Sodienthoai"] != DBNull.Value ? reader["Sodienthoai"].ToString() : null,
                                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                    // Đọc TEXT và chuyển đổi sang DateTime
                                    Ngayvaolam = DateTime.Parse(reader["Ngayvaolam"].ToString())
                                };
                                nhanviens.Add(nhanvien); // Thêm object vào danh sách
                            }
                        } // DataReader tự đóng khi thoát using
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (ví dụ: hiển thị MessageBox hoặc ghi log)
                    MessageBox.Show($"Lỗi khi lấy danh sách nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Tùy chọn: ném lại exception để lớp gọi xử lý
                    // throw;
                }
            } // Connection tự đóng khi thoát using
            return nhanviens; // Trả về danh sách nhân viên
        }

        // =====================================================
        // PHƯƠNG THỨC LẤY NHÂN VIÊN THEO ID
        // =====================================================

        /// <summary>
        /// Lấy thông tin nhân viên theo Mã nhân viên.
        /// </summary>
        /// <param name="manhanvien">Mã nhân viên cần lấy.</param>
        /// <returns>Đối tượng Nhanvien nếu tìm thấy, ngược lại trả về null.</returns>
        public Nhanvien GetNhanvienById(string manhanvien)
        {
            Nhanvien nhanvien = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Manhanvien, Hoten, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Email, Ngayvaolam FROM Nhanvien WHERE Manhanvien = @Manhanvien";

                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        // Sử dụng Parameter cho điều kiện WHERE
                        command.Parameters.AddWithValue("@Manhanvien", manhanvien);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Đọc hàng đầu tiên (và duy nhất) nếu có
                            {
                                nhanvien = new Nhanvien
                                {
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Hoten = reader["Hoten"].ToString(),
                                    Ngaysinh = DateTime.Parse(reader["Ngaysinh"].ToString()),
                                    Gioitinh = reader["Gioitinh"].ToString(),
                                    Diachi = reader["Diachi"].ToString(),
                                    Sodienthoai = reader["Sodienthoai"] != DBNull.Value ? reader["Sodienthoai"].ToString() : null,
                                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                    Ngayvaolam = DateTime.Parse(reader["Ngayvaolam"].ToString())
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy nhân viên theo ID: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
            return nhanvien; // Trả về object Nhanvien hoặc null
        }

        // =====================================================
        // PHƯƠNG THỨC THÊM NHÂN VIÊN MỚI
        // =====================================================

        /// <summary>
        /// Thêm một nhân viên mới vào CSDL.
        /// </summary>
        /// <param name="nhanvien">Đối tượng Nhanvien cần thêm.</param>
        public void AddNhanvien(Nhanvien nhanvien)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = @"
                        INSERT INTO Nhanvien (Manhanvien, Hoten, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Email, Ngayvaolam)
                        VALUES (@Manhanvien, @Hoten, @Ngaysinh, @Gioitinh, @Diachi, @Sodienthoai, @Email, @Ngayvaolam)";

                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        // Sử dụng Parameters để chèn dữ liệu
                        command.Parameters.AddWithValue("@Manhanvien", nhanvien.Manhanvien);
                        command.Parameters.AddWithValue("@Hoten", nhanvien.Hoten);
                        // Chuyển DateTime sang TEXT để lưu
                        command.Parameters.AddWithValue("@Ngaysinh", nhanvien.Ngaysinh.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Gioitinh", nhanvien.Gioitinh);
                        command.Parameters.AddWithValue("@Diachi", nhanvien.Diachi);
                        // Xử lý cột có thể NULL: nếu giá trị C# là null, chèn DBNull.Value
                        command.Parameters.AddWithValue("@Sodienthoai", (object)nhanvien.Sodienthoai ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)nhanvien.Email ?? DBNull.Value);
                        // Chuyển DateTime sang TEXT để lưu
                        command.Parameters.AddWithValue("@Ngayvaolam", nhanvien.Ngayvaolam.ToString("yyyy-MM-dd HH:mm:ss"));

                        command.ExecuteNonQuery(); // Thực thi lệnh INSERT
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
        }

        // =====================================================
        // PHƯƠNG THỨC CẬP NHẬT THÔNG TIN NHÂN VIÊN
        // =====================================================

        /// <summary>
        /// Cập nhật thông tin của một nhân viên.
        /// </summary>
        /// <param name="nhanvien">Đối tượng Nhanvien chứa thông tin cập nhật (cần có Manhanvien).</param>
        public void UpdateNhanvien(Nhanvien nhanvien)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string updateSql = @"
                        UPDATE Nhanvien
                        SET Hoten = @Hoten,
                            Ngaysinh = @Ngaysinh,
                            Gioitinh = @Gioitinh,
                            Diachi = @Diachi,
                            Sodienthoai = @Sodienthoai,
                            Email = @Email,
                            Ngayvaolam = @Ngayvaolam
                        WHERE Manhanvien = @Manhanvien"; // Cập nhật dựa trên khóa chính

                    using (SQLiteCommand command = new SQLiteCommand(updateSql, connection))
                    {
                        // Sử dụng Parameters để cập nhật dữ liệu
                        command.Parameters.AddWithValue("@Hoten", nhanvien.Hoten);
                        command.Parameters.AddWithValue("@Ngaysinh", nhanvien.Ngaysinh.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Gioitinh", nhanvien.Gioitinh);
                        command.Parameters.AddWithValue("@Diachi", nhanvien.Diachi);
                        command.Parameters.AddWithValue("@Sodienthoai", (object)nhanvien.Sodienthoai ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)nhanvien.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Ngayvaolam", nhanvien.Ngayvaolam.ToString("yyyy-MM-dd HH:mm:ss"));
                        // Parameter cho điều kiện WHERE
                        command.Parameters.AddWithValue("@Manhanvien", nhanvien.Manhanvien);

                        command.ExecuteNonQuery(); // Thực thi lệnh UPDATE
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
        }

        // =====================================================
        // PHƯƠNG THỨC XÓA NHÂN VIÊN
        // =====================================================

        /// <summary>
        /// Xóa một nhân viên khỏi CSDL.
        /// </summary>
        /// <param name="manhanvien">Mã nhân viên cần xóa.</param>
        public void DeleteNhanvien(string manhanvien)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string deleteSql = "DELETE FROM Nhanvien WHERE Manhanvien = @Manhanvien";

                    using (SQLiteCommand command = new SQLiteCommand(deleteSql, connection))
                    {
                        // Sử dụng Parameter cho điều kiện WHERE
                        command.Parameters.AddWithValue("@Manhanvien", manhanvien);

                        command.ExecuteNonQuery(); // Thực thi lệnh DELETE
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
        }

        // =====================================================
        // PHƯƠNG THỨC TÌM KIẾM NHÂN VIÊN
        // =====================================================

        /// <summary>
        /// Tìm kiếm nhân viên dựa trên từ khóa trong các cột Manhanvien, Hoten, Sodienthoai, Email, Diachi.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm.</param>
        /// <returns>Danh sách các đối tượng Nhanvien phù hợp.</returns>
        public List<Nhanvien> SearchNhanviens(string searchTerm)
        {
            List<Nhanvien> nhanviens = new List<Nhanvien>();

            // Kiểm tra nếu từ khóa rỗng hoặc null, trả về danh sách rỗng
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return nhanviens;
            }

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    // Câu lệnh SELECT với điều kiện WHERE sử dụng LIKE để tìm kiếm theo mẫu
                    // Tìm kiếm trong Manhanvien, Hoten, Sodienthoai, Email, Diachi
                    // Sử dụng LOWER() để tìm kiếm không phân biệt chữ hoa/thường
                    string selectSql = @"
                        SELECT Manhanvien, Hoten, Ngaysinh, Gioitinh, Diachi, Sodienthoai, Email, Ngayvaolam
                        FROM Nhanvien
                        WHERE LOWER(Manhanvien) LIKE @SearchTerm
                           OR LOWER(Hoten) LIKE @SearchTerm
                           OR LOWER(Diachi) LIKE @SearchTerm
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
                                Nhanvien nhanvien = new Nhanvien
                                {
                                    Manhanvien = reader["Manhanvien"].ToString(),
                                    Hoten = reader["Hoten"].ToString(),
                                    Ngaysinh = DateTime.Parse(reader["Ngaysinh"].ToString()),
                                    Gioitinh = reader["Gioitinh"].ToString(),
                                    Diachi = reader["Diachi"].ToString(),
                                    Sodienthoai = reader["Sodienthoai"] != DBNull.Value ? reader["Sodienthoai"].ToString() : null,
                                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                    Ngayvaolam = DateTime.Parse(reader["Ngayvaolam"].ToString())
                                };
                                nhanviens.Add(nhanvien);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // throw;
                }
            }
            return nhanviens; // Trả về danh sách nhân viên phù hợp
        }
    }
}
