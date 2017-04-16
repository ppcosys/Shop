namespace DeployApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("DeployCart.Product", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("DeployCart.Product", "Price");
        }
    }
}
