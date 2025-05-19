using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.Model
{
    public class Khachhang
    {
        [Key]
        public string Makhachhang { get; set; }
        public string Hoten { get; set; }
        public string Sodienthoai { get; set; }
        public string Email { get; set; }
        public DateTime Ngaydangky { get; set; }
        public int Diemtichluy { get; set; }
    }
}
