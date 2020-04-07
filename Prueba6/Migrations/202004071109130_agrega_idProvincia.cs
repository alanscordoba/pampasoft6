namespace pampasoft6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agrega_idProvincia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.localidades", "IdProvincia", c => c.Int(nullable: false));
            CreateIndex("dbo.localidades", "IdProvincia");
            Sql("update localidades set IdProvincia = 1 where id <> 0");
            AddForeignKey("dbo.localidades", "IdProvincia", "dbo.provincias", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.localidades", "IdProvincia", "dbo.provincias");
            DropIndex("dbo.localidades", new[] { "IdProvincia" });
            DropColumn("dbo.localidades", "IdProvincia");
        }
    }
}
