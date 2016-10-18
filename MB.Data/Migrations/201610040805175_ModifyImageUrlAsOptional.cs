namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyImageUrlAsOptional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "ImageUrl", c => c.String(maxLength: 250));
            AlterColumn("dbo.CarCate", "ImageUrl", c => c.String(maxLength: 250));
            AlterColumn("dbo.CityCate", "ImageUrl", c => c.String(maxLength: 250));
            AlterColumn("dbo.Category", "ImageUrl", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "ImageUrl", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.CityCate", "ImageUrl", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.CarCate", "ImageUrl", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Department", "ImageUrl");
        }
    }
}
