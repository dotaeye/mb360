namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHotAndSoldCountForProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "SoldCount", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Hot", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Hot");
            DropColumn("dbo.Product", "SoldCount");
        }
    }
}
