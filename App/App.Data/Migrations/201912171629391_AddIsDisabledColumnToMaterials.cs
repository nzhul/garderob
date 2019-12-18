namespace App.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDisabledColumnToMaterials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "IsDisabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materials", "IsDisabled");
        }
    }
}
