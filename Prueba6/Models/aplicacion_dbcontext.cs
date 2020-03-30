namespace pampasoft6
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Models;

    public partial class aplicacion_dbcontext : DbContext
    {
        public aplicacion_dbcontext(): base("name=ConexionMysql"){}
 
        public virtual DbSet<localidades> localidades { get; set; }
        public virtual DbSet<provincias> provincias { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // No pluraliza los nombres de las tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
