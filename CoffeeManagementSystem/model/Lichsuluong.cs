using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagementSystem
{
    public class Lichsuluong
    {
        [Key]
        public string Malichsuluong { get; set; }

        [ForeignKey("Nhanvien")]
        public string Manhanvien { get; set; }

        public decimal Mucluonggio { get; set; }
        public DateTime Ngayapdung { get; set; }
        public string Ghichu { get; set; }

        public Nhanvien Nhanvien { get; set; }
    }
}
