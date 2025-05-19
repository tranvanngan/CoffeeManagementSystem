using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCafe.Model
{
    public class Douong
    {
        [Key]
        public string Madouong { get; set; }
        public string Tendouong { get; set; }

        [ForeignKey("Loaidouong")]
        public string Maloai { get; set; }

        public string Mota { get; set; }
        public string Hinhanh { get; set; }

        public Loaidouong Loaidouong { get; set; }
        public ICollection<Giadouong> Giadouongs { get; set; }
        public ICollection<Chitietdonhang> Chitietdonhangs { get; set; }
    }

}
