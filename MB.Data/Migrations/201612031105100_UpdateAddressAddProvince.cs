namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAddressAddProvince : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Province", c => c.String(maxLength: 50));
            AddColumn("dbo.Address", "Area", c => c.String(maxLength: 50));
            AddColumn("dbo.Address", "County", c => c.String(maxLength: 50));
            AlterColumn("dbo.Address", "CityCodeList", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Address", "CityCodeList", c => c.String());
            DropColumn("dbo.Address", "County");
            DropColumn("dbo.Address", "Area");
            DropColumn("dbo.Address", "Province");
        }
    }
}
