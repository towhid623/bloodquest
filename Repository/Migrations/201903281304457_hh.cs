namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banners", "ImgUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Services", "IconClass", c => c.String(nullable: false));
            AlterColumn("dbo.Services", "ImgUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "ImgUrl", c => c.String());
            AlterColumn("dbo.Services", "IconClass", c => c.String());
            AlterColumn("dbo.Banners", "ImgUrl", c => c.String());
        }
    }
}
