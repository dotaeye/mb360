namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBannerImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "BannerUrl", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "BannerUrl");
        }
    }
}
