// Trong file: BLL/AuthBLL.cs

using CoffeeManagementSystem.DAL;
using CoffeeManagementSystem; // Chứa lớp Taikhoan (Model)
using System;

namespace CoffeeManagementSystem.BLL
{
    public class AuthBLL
    {
        private TaikhoanDAL _taikhoanDAL;
        private NhanvienDAL _nhanvienDAL; // Cần để lấy thông tin nhân viên

        public AuthBLL()
        {
            _taikhoanDAL = new TaikhoanDAL();
            _nhanvienDAL = new NhanvienDAL();
        }

        /// <summary>
        /// Xác thực người dùng và trả về thông tin tài khoản nếu đăng nhập thành công.
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập.</param>
        /// <param name="matKhau">Mật khẩu.</param>
        /// <returns>Đối tượng Taikhoan nếu đăng nhập thành công, ngược lại là null.</returns>
        /// <exception cref="ArgumentException">Ném ngoại lệ nếu tên đăng nhập hoặc mật khẩu rỗng.</exception>
        public Taikhoan AuthenticateUser(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                throw new ArgumentException("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
            }

            // Gọi DAL để kiểm tra tài khoản
            return _taikhoanDAL.GetTaikhoanByTendangnhapAndMatkhau(tenDangNhap, matKhau);
        }

        /// <summary>
        /// Lấy tên hiển thị của nhân viên dựa trên mã nhân viên trong tài khoản.
        /// </summary>
        /// <param name="maNhanVien">Mã nhân viên.</param>
        /// <param name="defaultName">Tên mặc định nếu không tìm thấy nhân viên.</param>
        /// <returns>Tên nhân viên hoặc tên mặc định.</returns>
        public string GetEmployeeDisplayName(string maNhanVien, string defaultName)
        {
            if (!string.IsNullOrEmpty(maNhanVien))
            {
                Nhanvien nhanVien = _nhanvienDAL.GetNhanvienById(maNhanVien);
                if (nhanVien != null && !string.IsNullOrEmpty(nhanVien.Hoten))
                {
                    return nhanVien.Hoten;
                }
            }
            return defaultName;
        }
    }
}