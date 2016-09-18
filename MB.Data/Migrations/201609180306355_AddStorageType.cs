namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStorageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storage", "StorageType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Storage", "StorageType");
        }
    }
}
