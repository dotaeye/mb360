namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressForStorage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storage", "Address", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Storage", "Address");
        }
    }
}
