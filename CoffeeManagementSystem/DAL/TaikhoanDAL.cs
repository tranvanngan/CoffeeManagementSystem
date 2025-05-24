using System;
using System.Collections.Generic;
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

            // ... Các phương thức khác của TaikhoanDAL (Add, Update, Delete, GetAll, Search)
        }

    }
