using pampasoft6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba6.Data.Repositorios
{
    public class ProvinciasRepositorio
    {
        public IEnumerable<SelectListItem> ObtenerListado()
        {
            using (AplicacionDbContext db = new AplicacionDbContext())
            {
                List<SelectListItem> c_provincias = db.provincias.AsNoTracking()
                    .OrderBy(n => n.Nombre)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.Nombre
                        }).ToList();

                //var nuevo = new SelectListItem()
                //{
                //    Value = null,
                //    Text = ""
                //};
                //c_provincias.Insert(0, nuevo);

                return new SelectList(c_provincias, "Value", "Text");
            }
        }

    }
}