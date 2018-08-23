using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pampasoft6.Services
{
    public class LocalidadesRepository
    {
        public List<localidades> ObtenerTodos()
        {
            using (var Db = new aplicacion_dbcontext())
            {
                return Db.localidades.ToList();
            }

        }

        internal void Crear(localidades model)
        {
            using (var db = new aplicacion_dbcontext())
            {
                db.localidades.Add(model);
                db.SaveChanges();
            }
        }
    }
}