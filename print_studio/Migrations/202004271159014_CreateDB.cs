namespace print_studio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ColorsProduct",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        color = c.String(nullable: false, maxLength: 20, unicode: false),
                        id_producttype = c.Int(),
                        id_image = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Image", t => t.id_image, cascadeDelete: true)
                .ForeignKey("dbo.ProductType", t => t.id_producttype, cascadeDelete: true)
                .Index(t => t.id_producttype)
                .Index(t => t.id_image);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SavedProduct",
                c => new
                    {
                        id = c.Int(nullable: false),
                        id_colorsproduct = c.Int(),
                        id_image = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Image", t => t.id_image)
                .ForeignKey("dbo.ColorsProduct", t => t.id_colorsproduct, cascadeDelete: true)
                .Index(t => t.id_colorsproduct)
                .Index(t => t.id_image);
            
            CreateTable(
                "dbo.PrintOrder",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(),
                        surname = c.String(nullable: false, maxLength: 50, unicode: false),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        adress = c.String(nullable: false, maxLength: 100, unicode: false),
                        phone = c.String(nullable: false, maxLength: 20, unicode: false),
                        id_size = c.Int(),
                        id_status = c.Int(),
                        id_employee = c.String(maxLength: 128),
                        id_savedproduct = c.Int(),
                        count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.id_employee)
                .ForeignKey("dbo.OrderStatus", t => t.id_status)
                .ForeignKey("dbo.Size", t => t.id_size)
                .ForeignKey("dbo.SavedProduct", t => t.id_savedproduct, cascadeDelete: true)
                .Index(t => t.id_size)
                .Index(t => t.id_status)
                .Index(t => t.id_employee)
                .Index(t => t.id_savedproduct);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Size",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 40, unicode: false),
                        material = c.String(nullable: false, maxLength: 40, unicode: false),
                        sq_m = c.Int(nullable: false),
                        description = c.String(maxLength: 256, unicode: false),
                        days = c.String(maxLength: 20, unicode: false),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SavedProduct", "id_colorsproduct", "dbo.ColorsProduct");
            DropForeignKey("dbo.ColorsProduct", "id_producttype", "dbo.ProductType");
            DropForeignKey("dbo.SavedProduct", "id_image", "dbo.Image");
            DropForeignKey("dbo.PrintOrder", "id_savedproduct", "dbo.SavedProduct");
            DropForeignKey("dbo.PrintOrder", "id_size", "dbo.Size");
            DropForeignKey("dbo.PrintOrder", "id_status", "dbo.OrderStatus");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrintOrder", "id_employee", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ColorsProduct", "id_image", "dbo.Image");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PrintOrder", new[] { "id_savedproduct" });
            DropIndex("dbo.PrintOrder", new[] { "id_employee" });
            DropIndex("dbo.PrintOrder", new[] { "id_status" });
            DropIndex("dbo.PrintOrder", new[] { "id_size" });
            DropIndex("dbo.SavedProduct", new[] { "id_image" });
            DropIndex("dbo.SavedProduct", new[] { "id_colorsproduct" });
            DropIndex("dbo.ColorsProduct", new[] { "id_image" });
            DropIndex("dbo.ColorsProduct", new[] { "id_producttype" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductType");
            DropTable("dbo.Size");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PrintOrder");
            DropTable("dbo.SavedProduct");
            DropTable("dbo.Image");
            DropTable("dbo.ColorsProduct");
        }
    }
}
