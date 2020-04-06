namespace pampasoft6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prueba2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.provincias", "nombre", c => c.String(maxLength: 100, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.provincias", "nombre", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
    }
}
