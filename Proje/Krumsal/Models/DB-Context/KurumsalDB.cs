namespace Kurumsal.Models.Sınıflar
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Kurumsal.Models.DB_Contect;

    public partial class KurumsalDB : DbContext
    {
        public KurumsalDB()
            : base("name=KurumsalDB")
        {
            Database.SetInitializer(new Initializer());

        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AltKategori> AltKategori { get; set; }
        public virtual DbSet<Hakkimizda> Hakkimizda { get; set; }
        public virtual DbSet<Hizmet> Hizmet { get; set; }
        public virtual DbSet<HizmetKategori> HizmetKategori { get; set; }
        public virtual DbSet<Iletisim> Iletisim { get; set; }
        public virtual DbSet<Kimlik> Kimlik { get; set; }
        public virtual DbSet<Mesaj> Mesaj { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AltKategori>()
                .HasMany(e => e.Hizmet)
                .WithOptional(e => e.AltKategori)
                .HasForeignKey(e => e.AltKategori_ID);
        }
    }
}
