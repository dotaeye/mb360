namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorCityCate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CityCate", "ParentId", "dbo.CityCate");
            DropForeignKey("dbo.Address", "CityId", "dbo.CityCate");
            DropIndex("dbo.Address", new[] { "CityId" });
            DropIndex("dbo.CityCate", new[] { "ParentId" });
            RenameColumn(table: "dbo.Address", name: "CityId", newName: "CityCode");
            RenameColumn(table: "dbo.CityCate", name: "ParentId", newName: "ParentCode");
            DropPrimaryKey("dbo.CityCate");
            AlterColumn("dbo.Address", "CityCode", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CityCate", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.CityCate", "ParentCode", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.CityCate", "Code");
            CreateIndex("dbo.Address", "CityCode");
            CreateIndex("dbo.CityCate", "ParentCode");
            AddForeignKey("dbo.CityCate", "ParentCode", "dbo.CityCate", "Code");
            AddForeignKey("dbo.Address", "CityCode", "dbo.CityCate", "Code");
            DropColumn("dbo.CityCate", "CreateUserId");
            DropColumn("dbo.CityCate", "CreateTime");
            DropColumn("dbo.CityCate", "LastUserId");
            DropColumn("dbo.CityCate", "LastTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CityCate", "LastTime", c => c.DateTime());
            AddColumn("dbo.CityCate", "LastUserId", c => c.String());
            AddColumn("dbo.CityCate", "CreateTime", c => c.DateTime());
            AddColumn("dbo.CityCate", "CreateUserId", c => c.String());
            DropForeignKey("dbo.Address", "CityCode", "dbo.CityCate");
            DropForeignKey("dbo.CityCate", "ParentCode", "dbo.CityCate");
            DropIndex("dbo.CityCate", new[] { "ParentCode" });
            DropIndex("dbo.Address", new[] { "CityCode" });
            DropPrimaryKey("dbo.CityCate");
            AlterColumn("dbo.CityCate", "ParentCode", c => c.Int());
            AlterColumn("dbo.CityCate", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Address", "CityCode", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CityCate", "Id");
            RenameColumn(table: "dbo.CityCate", name: "ParentCode", newName: "ParentId");
            RenameColumn(table: "dbo.Address", name: "CityCode", newName: "CityId");
            CreateIndex("dbo.CityCate", "ParentId");
            CreateIndex("dbo.Address", "CityId");
            AddForeignKey("dbo.Address", "CityId", "dbo.CityCate", "Id");
            AddForeignKey("dbo.CityCate", "ParentId", "dbo.CityCate", "Id");
        }
    }
}
