namespace Kurumsal.Models.Sınıflar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mesaj")]
    public partial class Mesaj
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string AdSoyad { get; set; }

        [StringLength(50)]
        public string Telefon { get; set; }

        [StringLength(100)]
        public string Konu { get; set; }

        public string İleti { get; set; }

        public string saat { get; set; }
    }
}
