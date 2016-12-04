namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityCodeListForAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "CityCodeList", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "CityCodeList");
        }
    }
}
