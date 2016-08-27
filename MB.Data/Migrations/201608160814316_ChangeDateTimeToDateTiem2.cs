namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimeToDateTiem2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Job", "CreateTime", c => c.DateTime());
            AlterColumn("dbo.Job", "LastTime", c => c.DateTime());
            AlterColumn("dbo.Department", "CreateTime", c => c.DateTime());
            AlterColumn("dbo.Department", "LastTime", c => c.DateTime());
            AlterColumn("dbo.UserActivities", "Time", c => c.DateTime());
            AlterColumn("dbo.UserRole", "CreateTime", c => c.DateTime());
            AlterColumn("dbo.UserRole", "LastTime", c => c.DateTime());
            AlterColumn("dbo.UserPermission", "CreateTime", c => c.DateTime());
            AlterColumn("dbo.UserPermission", "LastTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserPermission", "LastTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserPermission", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserRole", "LastTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserRole", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserActivities", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Department", "LastTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Department", "CreateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Job", "LastTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Job", "CreateTime", c => c.DateTime(nullable: false));
        }
    }
}
