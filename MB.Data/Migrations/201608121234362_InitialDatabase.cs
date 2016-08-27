namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Avatar = c.String(maxLength: 250),
                        Sex = c.Boolean(nullable: false),
                        PhonePrefix = c.String(maxLength: 50),
                        OpenId = c.String(maxLength: 250),
                        OpenTypeId = c.Int(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(nullable: false, maxLength: 150),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 150),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        UserPermission_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Job", t => t.JobId)
                .ForeignKey("dbo.UserPermission", t => t.UserPermission_Id)
                .ForeignKey("dbo.UserRole", t => t.UserRoleId)
                .Index(t => t.UserRoleId)
                .Index(t => t.JobId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.UserPermission_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        Code = c.String(nullable: false, maxLength: 50),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Deleted = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPermission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Controller = c.String(nullable: false, maxLength: 50),
                        Action = c.String(nullable: false, maxLength: 50),
                        IsApi = c.Boolean(nullable: false),
                        CreateUserId = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastUserId = c.String(),
                        LastTime = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole_Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionId, t.RoleId })
                .ForeignKey("dbo.UserPermission", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.UserRole", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserRoleId", "dbo.UserRole");
            DropForeignKey("dbo.UserRole_Permissions", "RoleId", "dbo.UserRole");
            DropForeignKey("dbo.UserRole_Permissions", "PermissionId", "dbo.UserPermission");
            DropForeignKey("dbo.AspNetUsers", "UserPermission_Id", "dbo.UserPermission");
            DropForeignKey("dbo.UserActivities", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "JobId", "dbo.Job");
            DropForeignKey("dbo.Job", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Department", "ParentId", "dbo.Department");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.UserRole_Permissions", new[] { "RoleId" });
            DropIndex("dbo.UserRole_Permissions", new[] { "PermissionId" });
            DropIndex("dbo.UserActivities", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Department", new[] { "ParentId" });
            DropIndex("dbo.Job", new[] { "DepartmentId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "UserPermission_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "JobId" });
            DropIndex("dbo.AspNetUsers", new[] { "UserRoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.UserRole_Permissions");
            DropTable("dbo.UserPermission");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserActivities");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Department");
            DropTable("dbo.Job");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
