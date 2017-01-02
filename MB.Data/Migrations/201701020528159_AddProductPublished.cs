namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductPublished : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Published", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "IsMatchAllCar", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "IsMatchAllCar");
            DropColumn("dbo.Product", "Published");
        }
    }
}
