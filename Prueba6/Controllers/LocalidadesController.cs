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

        public ActionResult Index(int? cp, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 8; // Debería ser por parámetro
            using (var db = new aplicacion_dbcontext())
            {
                Func<localidades, bool> predicado = x => !cp.HasValue || cp.Value == x.CP;

                var listadolocalidades = db.localidades.Where(predicado).OrderBy(x => x.N_LOCAL)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.localidades.Where(predicado).Count();

                var modelo = new Localidades_view();
                modelo.Localidades = listadolocalidades;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["cp"] = cp;
                return View(modelo);
            }
        }

        // GET: Localidades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Localidades/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Localidades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Localidades/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult BusquedaRapida(string term)
        {
            using (aplicacion_dbcontext db = new aplicacion_dbcontext())
            {
                var resultado = db.localidades.Where(x => x.N_LOCAL.Contains(term)).OrderBy(x => x.N_LOCAL)
                    .Select(x => x.N_LOCAL).Take(5).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
