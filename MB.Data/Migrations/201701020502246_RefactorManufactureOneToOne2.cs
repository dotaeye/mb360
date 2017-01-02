namespace MB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorManufactureOneToOne2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer");
            DropIndex("dbo.Product", new[] { "ManufacturerId" });
            AlterColumn("dbo.Product", "ManufacturerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "ManufacturerId");
            AddForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer");
            DropIndex("dbo.Product", new[] { "ManufacturerId" });
            AlterColumn("dbo.Product", "ManufacturerId", c => c.Int());
            CreateIndex("dbo.Product", "ManufacturerId");
            AddForeignKey("dbo.Product", "ManufacturerId", "dbo.Manufacturer", "Id");
        }
    }
}
