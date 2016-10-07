namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Deleted");
        }
    }
}
