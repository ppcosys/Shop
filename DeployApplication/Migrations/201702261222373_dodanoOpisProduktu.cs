namespace DeployApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanoOpisProduktu : DbMigration
    {
        public override void Up()
        {
            AddColumn("DeployCart.Product", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("DeployCart.Product", "Description");
        }
    }
}
