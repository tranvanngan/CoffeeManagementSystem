using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.Model
{
    public class Chamcong
    {
        [Key]
        public string Machamcong { get; set; }

        [ForeignKey("Nhanvien")]
        public string Manhanvien { get; set; }

        [ForeignKey("Calamviec")]
        public string Maca { get; set; }

        public DateTime Ngay { get; set; }

        public Nhanvien Nhanvien { get; set; }
        public Calamviec Calamviec { get; set; }
    }
}
