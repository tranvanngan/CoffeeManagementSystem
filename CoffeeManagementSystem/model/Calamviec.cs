using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagementSystem
{
    public class Calamviec
    {
        [Key]
        public string Maca { get; set; }
        public string Tenca { get; set; }
        public TimeSpan Thoigianbatdau { get; set; }
        public TimeSpan Thoigianketthuc { get; set; }
        public decimal Sogio { get; set; }
        public ICollection<Chamcong> Chamcongs { get; set; }
    }
}