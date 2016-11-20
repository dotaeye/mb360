namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateATypError : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "OwnerId", c => c.String(maxLength: 128));
            DropColumn("dbo.Product", "OwenerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "OwenerId", c => c.String(maxLength: 128));
            DropColumn("dbo.Product", "OwnerId");
        }
    }
}
