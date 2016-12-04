namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeChatField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "WeChatSign", c => c.String(maxLength: 350));
            AlterColumn("dbo.Order", "PrePayId", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "PrePayId", c => c.String());
            DropColumn("dbo.Order", "WeChatSign");
        }
    }
}
