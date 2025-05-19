using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.Model
{
    public class Luong
    {
        [Key]
        public string Maluong { get; set; }

        [ForeignKey("Nhanvien")]
        public string Manhanvien { get; set; }

        public int Thang { get; set; }
        public int Nam { get; set; }
        public int Tongca { get; set; }
        public decimal Tonggio { get; set; }
        public decimal LuongTong { get; set; }

        public Nhanvien Nhanvien { get; set; }
    }
}
