namespace DeployApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productPriceCollumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("DeployCart.Cart", "ProductPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("DeployCart.Cart", "ProductPrice");
        }
    }
}
