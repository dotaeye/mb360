namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevelForCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.CarCate", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.CityCate", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.Category", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Level");
            DropColumn("dbo.CityCate", "Level");
            DropColumn("dbo.CarCate", "Level");
            DropColumn("dbo.Department", "Level");
        }
    }
}
