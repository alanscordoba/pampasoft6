namespace pampasoft6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clase_TablaBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.localidades", "FechaCreacion", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.localidades", "FechaCreacion");
        }
    }
}
