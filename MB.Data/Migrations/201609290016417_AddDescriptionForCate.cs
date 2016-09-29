namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionForCate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarCate", "Description", c => c.String());
            AddColumn("dbo.CityCate", "Description", c => c.String());
            AddColumn("dbo.Category", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Description");
            DropColumn("dbo.CityCate", "Description");
            DropColumn("dbo.CarCate", "Description");
        }
    }
}
