namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupToPermission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPermission", "Group", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPermission", "Group");
        }
    }
}
