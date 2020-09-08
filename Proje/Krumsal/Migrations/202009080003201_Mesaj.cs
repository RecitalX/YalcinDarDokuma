namespace Kurumsal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mesaj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mesaj", "saat", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mesaj", "saat", c => c.Int(nullable: false));
        }
    }
}
