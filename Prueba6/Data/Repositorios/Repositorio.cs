using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using pampasoft6.Data;
using pampasoft6.Models;
using Pampasoft6.Models;

namespace pampasoft6.Data.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : TablaBase, new()
    {
        public void Actualizar(T tabla)
        {
            using (var db = new AplicacionDbContext())
            {
                tabla.FechaModificacion = DateTime.Now;
                db.Entry(tabla).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Agregar(T tabla)
        {
            using (var db = new AplicacionDbContext())
            {
                tabla.FechaCreacion = DateTime.Now;
                db.Entry(tabla).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new AplicacionDbContext())
            {
                var tabla = new T() { Id = id };
                db.Entry(tabla).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public IEnumerable<T> EncontrarPor(ParametrosDeQuery<T> parametrosDeQuery)
        {
            var orderByClass = ObtenerOrderBy(parametrosDeQuery);
            Expression<Func<T, bool>> whereTrue = x => true;
            var where = (parametrosDeQuery.Where == null) ? whereTrue : parametrosDeQuery.Where;
            var include = (parametrosDeQuery.Include);
            using (AplicacionDbContext db = new AplicacionDbContext())
            {
                if (orderByClass.IsAscending)
                {
                    return db.Set<T>().Include(include).Where(where).OrderBy(orderByClass.OrderBy)
                    .Skip((parametrosDeQuery.Pagina - 1) * parametrosDeQuery.Top)
                    .Take(parametrosDeQuery.Top).ToList();
                }
                else
                {
                    return db.Set<T>().Include(include).Where(where).OrderByDescending(orderByClass.OrderBy)
                    .Skip((parametrosDeQuery.Pagina - 1) * parametrosDeQuery.Top)
                    .Take(parametrosDeQuery.Top).ToList();
                }
            }
        }

        private OrderByClass ObtenerOrderBy(ParametrosDeQuery<T> parametrosDeQuery)
        {
            if (parametrosDeQuery.OrderBy == null && parametrosDeQuery.OrderByDescending == null)
            {
                return new OrderByClass(x => x.Id, true);
            }

            return (parametrosDeQuery.OrderBy != null)
                ? new OrderByClass(parametrosDeQuery.OrderBy, true) :
                new OrderByClass(parametrosDeQuery.OrderByDescending, false);
        }

        public T ObtenerPorId(int id, ParametrosDeQuery<T> parametrosDeQuery)
        {
            var include = (parametrosDeQuery.Include);
            using (var db = new AplicacionDbContext())
            {
                return db.Set<T>().Include(include).FirstOrDefault(x => x.Id == id);
            }
        }

        public int Contar(Expression<Func<T, bool>> where)
        {
            using (var db = new AplicacionDbContext())
            {
                return db.Set<T>().Where(where).Count();
            }
        }

        private class OrderByClass
        {

            public OrderByClass()
            {

            }

            public OrderByClass(Func<T, object> orderBy, bool isAscending)
            {
                OrderBy = orderBy;
                IsAscending = isAscending;
            }


            public Func<T, object> OrderBy { get; set; }
            public bool IsAscending { get; set; }
        }
    }
}