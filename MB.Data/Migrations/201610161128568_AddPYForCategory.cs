namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPYForCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "PingYin", c => c.String());
            AddColumn("dbo.CarCate", "PingYin", c => c.String());
            AddColumn("dbo.CityCate", "PingYin", c => c.String());
            AddColumn("dbo.Category", "PingYin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "PingYin");
            DropColumn("dbo.CityCate", "PingYin");
            DropColumn("dbo.CarCate", "PingYin");
            DropColumn("dbo.Department", "PingYin");
        }
    }
}
