namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageUrlForCate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarCate", "ImageUrl", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.CarCate", "DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.CityCate", "ImageUrl", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Category", "ImageUrl", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Category", "DisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "DisplayOrder");
            DropColumn("dbo.Category", "ImageUrl");
            DropColumn("dbo.CityCate", "ImageUrl");
            DropColumn("dbo.CarCate", "DisplayOrder");
            DropColumn("dbo.CarCate", "ImageUrl");
        }
    }
}
