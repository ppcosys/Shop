namespace DeployApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("DeployCart.Cart", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("DeployCart.Cart", "UserId");
        }
    }
}
