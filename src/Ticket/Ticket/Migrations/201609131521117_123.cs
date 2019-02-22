namespace Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permits",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 10),
                        Description = c.String(nullable: false, maxLength: 200),
                        Quarter_price = c.Single(nullable: false),
                        HalfYear_price = c.Single(nullable: false),
                        Year_price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Type);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        Cost = c.Single(nullable: false),
                        PTime = c.DateTime(nullable: false),
                        Type_Type = c.String(nullable: false, maxLength: 10),
                        UserName_UserName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PId)
                .ForeignKey("dbo.Permits", t => t.Type_Type, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserName_UserName, cascadeDelete: true)
                .Index(t => t.Type_Type)
                .Index(t => t.UserName_UserName);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 30),
                        GName = c.String(maxLength: 20),
                        SName = c.String(maxLength: 20),
                        Address = c.String(maxLength: 40),
                        State = c.String(maxLength: 20),
                        PostCode = c.String(maxLength: 4),
                        Mobile = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "UserName_UserName", "dbo.Users");
            DropForeignKey("dbo.Purchases", "Type_Type", "dbo.Permits");
            DropIndex("dbo.Purchases", new[] { "UserName_UserName" });
            DropIndex("dbo.Purchases", new[] { "Type_Type" });
            DropTable("dbo.Users");
            DropTable("dbo.Purchases");
            DropTable("dbo.Permits");
        }
    }
}
