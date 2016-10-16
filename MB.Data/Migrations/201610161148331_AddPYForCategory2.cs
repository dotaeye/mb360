namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPYForCategory2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "PinYin", c => c.String());
            AddColumn("dbo.CarCate", "PinYin", c => c.String());
            AddColumn("dbo.CityCate", "PinYin", c => c.String());
            AddColumn("dbo.Category", "PinYin", c => c.String());
            DropColumn("dbo.Department", "PingYin");
            DropColumn("dbo.CarCate", "PingYin");
            DropColumn("dbo.CityCate", "PingYin");
            DropColumn("dbo.Category", "PingYin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Category", "PingYin", c => c.String());
            AddColumn("dbo.CityCate", "PingYin", c => c.String());
            AddColumn("dbo.CarCate", "PingYin", c => c.String());
            AddColumn("dbo.Department", "PingYin", c => c.String());
            DropColumn("dbo.Category", "PinYin");
            DropColumn("dbo.CityCate", "PinYin");
            DropColumn("dbo.CarCate", "PinYin");
            DropColumn("dbo.Department", "PinYin");
        }
    }
}
