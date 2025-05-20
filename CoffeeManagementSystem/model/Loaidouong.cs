using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoffeeManagementSystem
{
    public class Loaidouong
    {
        [Key]
        public string Maloai { get; set; }
        public string Tenloai { get; set; }
        public ICollection<Douong> Douongs { get; set; }
    }

}
