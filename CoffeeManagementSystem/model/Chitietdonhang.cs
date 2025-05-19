using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCafe.Model
{
    public class Chitietdonhang
    {
        [Key]
        public string Machitiet { get; set; }

        [ForeignKey("Donhang")]
        public string Madonhang { get; set; }

        [ForeignKey("Douong")]
        public string Madouong { get; set; }

        public int Soluong { get; set; }
        public decimal Dongia { get; set; }
        public decimal Thanhtien { get; set; }
        public string Ghichu { get; set; }

        public Donhang Donhang { get; set; }
        public Douong Douong { get; set; }
    }
}
