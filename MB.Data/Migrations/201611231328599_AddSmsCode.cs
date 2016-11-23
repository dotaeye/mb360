namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSmsCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmsCode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 20),
                        CodeTypeId = c.Int(nullable: false),
                        CodeType = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ExpireTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmsCode");
        }
    }
}
