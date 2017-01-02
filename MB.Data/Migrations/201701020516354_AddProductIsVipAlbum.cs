namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductIsVipAlbum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsVipAlbum", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "IsVipAlbum");
        }
    }
}
