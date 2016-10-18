namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateInfoForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastLoginTime", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "CreateUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreateTime", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastTime");
            DropColumn("dbo.AspNetUsers", "LastUserId");
            DropColumn("dbo.AspNetUsers", "CreateTime");
            DropColumn("dbo.AspNetUsers", "CreateUserId");
            DropColumn("dbo.AspNetUsers", "LastLoginTime");
        }
    }
}
