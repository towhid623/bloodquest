namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "ImgUrl", c => c.String(nullable: false));
        }
    }
}
