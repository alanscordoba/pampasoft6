namespace pampasoft6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class borra_campo_fecha : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.localidades", "FechaCreacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.localidades", "FechaCreacion", c => c.DateTime(nullable: false, precision: 0));
        }
    }
}
