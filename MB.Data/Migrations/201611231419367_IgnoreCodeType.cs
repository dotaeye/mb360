namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IgnoreCodeType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SmsCode", "CodeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SmsCode", "CodeType", c => c.Int(nullable: false));
        }
    }
}
