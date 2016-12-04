namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrePayIdForWeChatPay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "PrePayId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "PrePayId");
        }
    }
}
