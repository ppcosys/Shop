namespace DeployApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFilenameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("DeployCart.Product", "PictureFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("DeployCart.Product", "PictureFileName");
        }
    }
}
