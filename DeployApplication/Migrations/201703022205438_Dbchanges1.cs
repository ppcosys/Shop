namespace DeployApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dbchanges1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("DeployCart.Product", "Cart_Id", "DeployCart.Cart");
            DropIndex("DeployCart.Product", new[] { "Cart_Id" });
            AddColumn("DeployCart.Cart", "ProductId", c => c.Int(nullable: false));
            AddColumn("DeployCart.Cart", "SessionId", c => c.String());
            AddColumn("DeployCart.Product", "Product_Id", c => c.Int());
            CreateIndex("DeployCart.Product", "Product_Id");
            AddForeignKey("DeployCart.Product", "Product_Id", "DeployCart.Product", "Id");
            DropColumn("DeployCart.Product", "Cart_Id");
        }
        
        public override void Down()
        {
            AddColumn("DeployCart.Product", "Cart_Id", c => c.Int());
            DropForeignKey("DeployCart.Product", "Product_Id", "DeployCart.Product");
            DropIndex("DeployCart.Product", new[] { "Product_Id" });
            DropColumn("DeployCart.Product", "Product_Id");
            DropColumn("DeployCart.Cart", "SessionId");
            DropColumn("DeployCart.Cart", "ProductId");
            CreateIndex("DeployCart.Product", "Cart_Id");
            AddForeignKey("DeployCart.Product", "Cart_Id", "DeployCart.Cart", "Id");
        }
    }
}
