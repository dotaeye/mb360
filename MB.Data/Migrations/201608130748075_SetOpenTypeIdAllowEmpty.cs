namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetOpenTypeIdAllowEmpty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "OpenTypeId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "OpenTypeId", c => c.Int(nullable: false));
        }
    }
}
