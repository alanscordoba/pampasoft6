namespace pampasoft6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agrega_fechamodificacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.localidades", "FechaModificacion", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.provincias", "FechaModificacion", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.provincias", "FechaModificacion");
            DropColumn("dbo.localidades", "FechaModificacion");
        }
    }
}
