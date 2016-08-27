namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRole : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRole", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRole", "UserId", c => c.String());
        }
    }
}
