namespace Kurumsal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OLustur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Eposta = c.String(maxLength: 250),
                        Sifre = c.String(maxLength: 250),
                        Yetki = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.AltKategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(maxLength: 100),
                        HizmetKategoriId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HizmetKategori", t => t.HizmetKategoriId)
                .Index(t => t.HizmetKategoriId);
            
            CreateTable(
                "dbo.Hizmet",
                c => new
                    {
                        HizmetId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 200),
                        Icerik = c.String(),
                        ResimURL = c.String(maxLength: 250),
                        UrunKodu = c.String(maxLength: 50),
                        RenkAdi = c.String(),
                        AltKategori_ID = c.Int(),
                    })
                .PrimaryKey(t => t.HizmetId)
                .ForeignKey("dbo.AltKategori", t => t.AltKategori_ID)
                .Index(t => t.AltKategori_ID);
            
            CreateTable(
                "dbo.HizmetKategori",
                c => new
                    {
                        HizmetKategoriId = c.Int(nullable: false, identity: true),
                        HizmetKategoriAdi = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.HizmetKategoriId);
            
            CreateTable(
                "dbo.Hakkimizda",
                c => new
                    {
                        HakkimizdaId = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.HakkimizdaId);
            
            CreateTable(
                "dbo.Iletisim",
                c => new
                    {
                        IletisimId = c.Int(nullable: false, identity: true),
                        Telefon = c.String(),
                        Mail = c.String(),
                        Adres = c.String(),
                    })
                .PrimaryKey(t => t.IletisimId);
            
            CreateTable(
                "dbo.Kimlik",
                c => new
                    {
                        KimlikId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Keywords = c.String(),
                        Description = c.String(),
                        LogoURL = c.String(),
                        ResimURL = c.String(),
                    })
                .PrimaryKey(t => t.KimlikId);
            
            CreateTable(
                "dbo.Mesaj",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(maxLength: 100),
                        Telefon = c.String(maxLength: 50),
                        Konu = c.String(maxLength: 100),
                        İleti = c.String(),
                        saat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AltKategori", "HizmetKategoriId", "dbo.HizmetKategori");
            DropForeignKey("dbo.Hizmet", "AltKategori_ID", "dbo.AltKategori");
            DropIndex("dbo.Hizmet", new[] { "AltKategori_ID" });
            DropIndex("dbo.AltKategori", new[] { "HizmetKategoriId" });
            DropTable("dbo.Mesaj");
            DropTable("dbo.Kimlik");
            DropTable("dbo.Iletisim");
            DropTable("dbo.Hakkimizda");
            DropTable("dbo.HizmetKategori");
            DropTable("dbo.Hizmet");
            DropTable("dbo.AltKategori");
            DropTable("dbo.Admin");
        }
    }
}
