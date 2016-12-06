namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNonceStrAndTimeSpanForOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OutTradeNo", c => c.String(maxLength: 50));
            AddColumn("dbo.Order", "TimeSpan", c => c.String(maxLength: 30));
            AddColumn("dbo.Order", "NonceStr", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "NonceStr");
            DropColumn("dbo.Order", "TimeSpan");
            DropColumn("dbo.Order", "OutTradeNo");
        }
    }
}
