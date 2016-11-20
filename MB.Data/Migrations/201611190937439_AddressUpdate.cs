namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Address", "UserId");
            AddForeignKey("dbo.Address", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Address", new[] { "UserId" });
            DropColumn("dbo.Address", "UserId");
        }
    }
}
