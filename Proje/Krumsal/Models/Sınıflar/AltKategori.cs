namespace Kurumsal.Models.Sınıflar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AltKategori")]
    public partial class AltKategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AltKategori()
        {
            Hizmet = new HashSet<Hizmet>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string KategoriAdi { get; set; }

        public int? HizmetKategoriId { get; set; }

        public virtual HizmetKategori HizmetKategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hizmet> Hizmet { get; set; }
    }
}
