namespace Kurumsal.Models.Sınıflar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hizmet")]
    public partial class Hizmet
    {
        public int HizmetId { get; set; }

        [StringLength(200)]
        public string Baslik { get; set; }

        public string Icerik { get; set; }

        [StringLength(250)]
        public string ResimURL { get; set; }

        [StringLength(50)]
        public string UrunKodu { get; set; }

        public string RenkAdi { get; set; }

        public int? AltKategori_ID { get; set; }

        public virtual AltKategori AltKategori { get; set; }
    }
}
