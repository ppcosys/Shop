namespace DeployApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDBDeployCartSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DeployCart.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DeployCart.Product",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("DeployCart.Cart", t => t.Cart_Id)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "DeployCart.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DeployCart.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("DeployCart.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("DeployCart.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "DeployCart.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DeployCart.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DeployCart.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "DeployCart.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("DeployCart.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DeployCart.IdentityUserRole", "ApplicationUser_Id", "DeployCart.ApplicationUser");
            DropForeignKey("DeployCart.IdentityUserLogin", "ApplicationUser_Id", "DeployCart.ApplicationUser");
            DropForeignKey("DeployCart.IdentityUserClaim", "ApplicationUser_Id", "DeployCart.ApplicationUser");
            DropForeignKey("DeployCart.IdentityUserRole", "IdentityRole_Id", "DeployCart.IdentityRole");
            DropForeignKey("DeployCart.Product", "Cart_Id", "DeployCart.Cart");
            DropIndex("DeployCart.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("DeployCart.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("DeployCart.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("DeployCart.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("DeployCart.Product", new[] { "Cart_Id" });
            DropTable("DeployCart.IdentityUserLogin");
            DropTable("DeployCart.IdentityUserClaim");
            DropTable("DeployCart.ApplicationUser");
            DropTable("DeployCart.IdentityUserRole");
            DropTable("DeployCart.IdentityRole");
            DropTable("DeployCart.Product");
            DropTable("DeployCart.Cart");
        }
    }
}
