using pampasoft6.Data;
using pampasoft6.Data.Repositorios;
using pampasoft6.ViewsModels;
using Prueba6.Data.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace pampasoft6.Controllers
{
    public class LocalidadesController : Controller
    {
        
        private AplicacionDbContext db = new AplicacionDbContext();
       // private LocalidadesRepository _repo;
        
        private IRepositorio<localidades> _repositorio;

        //private List<SelectListItem> _listadoprovincias;

        public LocalidadesController(IRepositorio<localidades> repositorio)
        {
            _repositorio = repositorio;
        //  _repo = new LocalidadesRepository();
        }

        //public LocalidadesController()
        //{
        //    _repo = new LocalidadesRepository();
        //}
        // GET: Localidades

        //public ActionResult Index()
        //{
        //    var model = _repo.ObtenerTodos();
        //    return View(model);
        //}

        public ActionResult Index(int? codigopostal, int pagina = 1)
        {

            ViewBag.Titulo = "Localidades";

            //var cantidadRegistrosPorPagina = 8; // Debería ser por parámetro
            //using (var db = new AplicacionDbContext())
            //{
            //    Func<localidades, bool> predicado = x => !codigopostal.HasValue || codigopostal.Value == x.CodigoPostal;

            //    var listadolocalidades = db.localidades.Where(predicado).OrderBy(x => x.Nombre)
            //        .Skip((pagina - 1) * cantidadRegistrosPorPagina)
            //        .Take(cantidadRegistrosPorPagina).ToList();
            //    var totalDeRegistros = db.localidades.Where(predicado).Count();

            //    var modelo = new Localidades_view();
            //    modelo.Localidades = listadolocalidades;
            //    modelo.PaginaActual = pagina;
            //    modelo.TotalDeRegistros = totalDeRegistros;
            //    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            //    modelo.ValoresQueryString = new RouteValueDictionary();
            //    modelo.ValoresQueryString["codigopostal"] = codigopostal;
            //    return View(modelo);
            //}

            var cantidadRegistrosPorPagina = 10; // Debería ser por parámetro
            var parametros = new ParametrosDeQuery<localidades>(pagina,cantidadRegistrosPorPagina);
            parametros.OrderBy = x => x.Nombre;
            parametros.Include = "Provincia";

            var c_tabla = _repositorio.EncontrarPor(parametros).ToList();
            var totalDeRegistros = _repositorio.Contar(x => !codigopostal.HasValue || codigopostal.Value == x.CodigoPostal);

            var modelo = new Localidades_view();
            modelo.Localidades = c_tabla;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
            modelo.ValoresQueryString = new RouteValueDictionary();
            modelo.ValoresQueryString["codigopostal"] = codigopostal;
            return View(modelo);
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            ViewBag.Titulo = "Localidad";

            {
                // Se Carga el DropDownList de Provincias
                //var c_provincias = db.provincias.OrderBy(x => x.Nombre).ToList();
                //_listadoprovincias = new List<SelectListItem>();
                //foreach (var item in c_provincias)
                //{
                //    _listadoprovincias.Add(new SelectListItem
                //    {
                //        Text = item.Nombre,
                //        Value = item.Id.ToString()
                //    });
                //}
                //ViewBag.ListadoProvincias = _listadoprovincias;

                var _listado = new ProvinciasRepositorio();
                ViewBag.ListadoProvincias = _listado.ObtenerListado();
            }
            return View();
        }

        // POST: Localidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(localidades c_tabla)
        {
            try
            {
               if (ModelState.IsValid)
               {
                    //_repo.Crear(model);
                    _repositorio.Agregar(c_tabla);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            // catch (Exception ex)
            {
                // log ex
            }
            //return View(model);
            return View();
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(int Id)
        {
            ViewBag.Titulo = "Localidad";

            var parametros = new ParametrosDeQuery<localidades>(0, 1);
            parametros.Include = "Provincia";

            var c_tabla = _repositorio.ObtenerPorId(Id, parametros);
            if (c_tabla == null)
            {
                return HttpNotFound();
            }

            var c_listado = new ProvinciasRepositorio();
            ViewBag.ListadoProvincias = c_listado.ObtenerListado();

            return View(c_tabla);

            // Se Carga el DropDownList de Provincias
            //var c_provincias = db.provincias.OrderBy(x => x.Nombre).ToList();
            //_listadoprovincias = new List<SelectListItem>();
            //foreach (var item in c_provincias)
            //{
            //    _listadoprovincias.Add(new SelectListItem
            //    {
            //        Text = item.Nombre,
            //        Value = item.Id.ToString()
            //    });
            //}
            //ViewBag.ListadoProvincias = _listadoprovincias;



            //if (Id == null)
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            //}

            //localidades localidades = db.localidades.Find(Id);
            //if (localidades == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(localidades);

            //var _listado = new ProvinciasRepositorio();
            //ViewBag.ListadoProvincias = _listado.ObtenerListado();
            //return View("Create",c_tabla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,CodigoPostal,Discado,IdProvincia")] localidades c_tabla)
        {
            if (ModelState.IsValid)
            {
                //    db.Entry(c_tabla).State = EntityState.Modified;
                //    db.SaveChanges();

                _repositorio.Actualizar(c_tabla);
                return RedirectToAction("Index");
            }
            return View(c_tabla);
        }

        public ActionResult Delete(int Id)
        {
            ViewBag.Tabla  = "Localidad";

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            //}
            //localidades localidades = db.localidades.Find(id);
            //if (localidades == null)
            //{
            //   return HttpNotFound();
            //}
            //return View(localidades);

            var parametros = new ParametrosDeQuery<localidades>(0,1);
            parametros.Include = "Provincia";

            var c_tabla = _repositorio.ObtenerPorId(Id,parametros);
            if (c_tabla == null)
            {
               return HttpNotFound();
            }
            return View();
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            //localidades localidades = db.localidades.Find(id);
            //db.localidades.Remove(localidades);
            //db.SaveChanges();
            //return RedirectToAction("Index"); 

            _repositorio.Eliminar(Id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            ViewBag.Tabla = "Localidad";
            ViewBag.Accion = "Detalles";

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            //}
            //localidades localidades = db.localidades.Find(id);
            //if (localidades == null)
            //{
            //   return HttpNotFound();
            //}
            //return View(localidades);

            var parametros = new ParametrosDeQuery<localidades>(0, 1);
            parametros.Include = "Provincia";

            var c_tabla = _repositorio.ObtenerPorId(Id,parametros);
            if (c_tabla == null)
            {
                return HttpNotFound();
            }
            return View(c_tabla);
        }

        public JsonResult BusquedaRapida(string term)
        {
            using (AplicacionDbContext db = new AplicacionDbContext())
            {
                var resultado = db.localidades.Where(x => x.Nombre.Contains(term)).OrderBy(x => x.Nombre)
                    .Select(x => x.Nombre).Take(5).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Crud(int Id = 0)
        {
            var c_listado = new ProvinciasRepositorio();
            ViewBag.ListadoProvincias = c_listado.ObtenerListado();

            if (Id == 0)
            {
                return View();
            }
            else
            {
                var parametros = new ParametrosDeQuery<localidades>(0, 1);
                parametros.Include = "Provincia";

                var c_tabla = _repositorio.ObtenerPorId(Id, parametros);
                if (c_tabla == null)
                {
                    return HttpNotFound();
                }
                return View(c_tabla);
            }
        }
        public ActionResult Guardar(localidades c_tabla)
        {
            if (ModelState.IsValid)
            {
                if (c_tabla.Id == 0)
                {
                    _repositorio.Agregar(c_tabla);
                }
                else
                {
                    _repositorio.Actualizar(c_tabla);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View("~/Views/Localidades/Crud.cshtml",c_tabla);
            }
            
        }

        public ActionResult Eliminar(int Id)
        {
            _repositorio.Eliminar(Id);
            return RedirectToAction("Index");
        }    


    }
}
