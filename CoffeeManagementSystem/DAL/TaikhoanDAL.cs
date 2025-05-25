using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagementSystem.DAL
{
    
        public partial class TaikhoanDAL : BaseDataAccess
        {
            // Constructor
            public TaikhoanDAL() : base() { }

            /// <summary>
            /// Lấy thông tin tài khoản dựa trên tên đăng nhập và mật khẩu.
            /// </summary>
            /// <param name="tendangnhap">Tên đăng nhập.</param>
            /// <param name="matkhau">Mật khẩu.</param>
            /// <returns>Đối tượng Taikhoan nếu tìm thấy, ngược lại là null.</returns>
            public Taikhoan GetTaikhoanByTendangnhapAndMatkhau(string tendangnhap, string matkhau)
            {
                Taikhoan taiKhoan = null;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        string selectSql = "SELECT Mataikhoan, Tendangnhap, Matkhau, Vaitro, Manhanvien FROM Taikhoan WHERE Tendangnhap = @Tendangnhap AND Matkhau = @Matkhau";
                        using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                        {
                            command.Parameters.AddWithValue("@Tendangnhap", tendangnhap);
                            command.Parameters.AddWithValue("@Matkhau", matkhau); // Lưu ý: Trong ứng dụng thực tế, không nên lưu mật khẩu dạng plaintext. Hãy dùng hashing.

                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    taiKhoan = new Taikhoan
                                    {
                                        Mataikhoan = reader["Mataikhoan"].ToString(),
                                        Tendangnhap = reader["Tendangnhap"].ToString(),
                                        Matkhau = reader["Matkhau"].ToString(),
                                        Vaitro = reader["Vaitro"].ToString(),
                                        Manhanvien = reader["Manhanvien"] != DBNull.Value ? reader["Manhanvien"].ToString() : null
                                    };
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi kiểm tra tài khoản: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // throw; // Có thể ném lỗi để lớp gọi xử lý
                    }
                }
                return taiKhoan;
            }

        // Phương thức để lấy Tên nhân viên từ Mã nhân viên
        public string GetTenNhanVienByMaNhanVien(string maNhanVien)
        {
            string tenNhanVien = null;
            string query = "SELECT TenNhanVien FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    connection.Open();
                    object result = command.ExecuteScalar(); // Lấy giá trị đầu tiên của cột đầu tiên
                    if (result != null)
                    {
                        tenNhanVien = result.ToString();
                    }
                }
            }
            return tenNhanVien;
        }
        public Taikhoan GetTaikhoanByManhanvien(string manhanvien)
        {
            Taikhoan taiKhoan = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string selectSql = "SELECT Mataikhoan, Tendangnhap, Matkhau, Vaitro, Manhanvien FROM Taikhoan WHERE Manhanvien = @Manhanvien";
                    using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
                    {
                        command.Parameters.AddWithValue("@Manhanvien", manhanvien);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                taiKhoan = new Taikhoan
                                {
                                    Mataikhoan = reader["Mataikhoan"].ToString(),
                                    Tendangnhap = reader["Tendangnhap"].ToString(),
                                    Matkhau = reader["Matkhau"].ToString(),
                                    Vaitro = reader["Vaitro"].ToString(),
                                    Manhanvien = reader["Manhanvien"] != DBNull.Value ? reader["Manhanvien"].ToString() : null
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy tài khoản theo mã nhân viên: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return taiKhoan;
        }
        public void AddTaikhoan(Taikhoan taikhoan)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string insertSql = @"
                        INSERT INTO Taikhoan (Mataikhoan, Tendangnhap, Matkhau, Vaitro, Manhanvien)
                        VALUES (@Mataikhoan, @Tendangnhap, @Matkhau, @Vaitro, @Manhanvien)";

                    using (SQLiteCommand command = new SQLiteCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@Mataikhoan", taikhoan.Mataikhoan);
                        command.Parameters.AddWithValue("@Tendangnhap", taikhoan.Tendangnhap);
                        command.Parameters.AddWithValue("@Matkhau", taikhoan.Matkhau); // Mật khẩu plaintext
                        command.Parameters.AddWithValue("@Vaitro", taikhoan.Vaitro);
                        command.Parameters.AddWithValue("@Manhanvien", (object)taikhoan.Manhanvien ?? DBNull.Value);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm tài khoản: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }
        public void UpdateTaikhoan(Taikhoan taikhoan)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string updateSql = @"
                        UPDATE Taikhoan
                        SET Tendangnhap = @Tendangnhap,
                            Matkhau = @Matkhau,
                            Vaitro = @Vaitro,
                            Manhanvien = @Manhanvien
                        WHERE Mataikhoan = @Mataikhoan";

                    using (SQLiteCommand command = new SQLiteCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue("@Tendangnhap", taikhoan.Tendangnhap);
                        command.Parameters.AddWithValue("@Matkhau", taikhoan.Matkhau); // Mật khẩu plaintext
                        command.Parameters.AddWithValue("@Vaitro", taikhoan.Vaitro);
                        command.Parameters.AddWithValue("@Manhanvien", (object)taikhoan.Manhanvien ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Mataikhoan", taikhoan.Mataikhoan);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật tài khoản: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }
        // ... Các phương thức khác của TaikhoanDAL (Add, Update, Delete, GetAll, Search)
    }

    }
