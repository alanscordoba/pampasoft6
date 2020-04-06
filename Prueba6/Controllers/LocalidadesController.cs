using pampasoft6.Services;
using pampasoft6.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace pampasoft6.Controllers
{
    public class LocalidadesController : Controller
    {
        private aplicacion_dbcontext db = new aplicacion_dbcontext();
        private LocalidadesRepository _repo;

        public LocalidadesController()
        {
            _repo = new LocalidadesRepository();
        }
        // GET: Localidades
        
        //public ActionResult Index()
        //{
        //    var model = _repo.ObtenerTodos();
        //    return View(model);
        //}

        public ActionResult Index(int? codigopostal, int pagina = 1)
        {

            ViewBag.Titulo = "Localidades"; 

            var cantidadRegistrosPorPagina = 8; // Debería ser por parámetro
            using (var db = new aplicacion_dbcontext())
            {
                Func<localidades, bool> predicado = x => !codigopostal.HasValue || codigopostal.Value == x.CodigoPostal;

                var listadolocalidades = db.localidades.Where(predicado).OrderBy(x => x.Nombre)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.localidades.Where(predicado).Count();

                var modelo = new Localidades_view();
                modelo.Localidades = listadolocalidades;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["codigopostal"] = codigopostal;
                return View(modelo);
            }
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            ViewBag.Titulo = "Crear Localidad";
            return View();
        }

        // POST: Localidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(localidades model)
        {
            try
            {
               if (ModelState.IsValid)
               {
                    _repo.Crear(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            // catch (Exception ex)
            {
                // log ex
            }
            return View(model);
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Titulo = "Modificar Localidad";
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            localidades localidades = db.localidades.Find(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            return View(localidades);
        }

        // POST: Localidades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,CodigoPostal,Discado")] localidades localidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localidades).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localidades);
        }


        // GET: Localidades/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Titulo = "Eliminar Localidad";
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            localidades localidades = db.localidades.Find(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            return View(localidades);
        }

        // POST: Localidades/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            localidades localidades = db.localidades.Find(id);
            db.localidades.Remove(localidades);
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }

        public ActionResult Details(int? id)
        {
            ViewBag.Titulo = "Detalles Localidad";
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            localidades localidades = db.localidades.Find(id);
            if (localidades == null)
            {
                return HttpNotFound();
            }
            return View(localidades);
        }

        public JsonResult BusquedaRapida(string term)
        {
            using (aplicacion_dbcontext db = new aplicacion_dbcontext())
            {
                var resultado = db.localidades.Where(x => x.Nombre.Contains(term)).OrderBy(x => x.Nombre)
                    .Select(x => x.Nombre).Take(5).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
