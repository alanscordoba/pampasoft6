using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using pampasoft6.Models;

namespace pampasoft6.Data
{
    public interface IRepositorio<T>
    {
        void Agregar(T TablaBase);
        void Eliminar(int Id);
        void Actualizar(T TablaBase);
        int Contar(Expression<Func<T, bool>> where);
        T ObtenerPorId(int id);
        IEnumerable<T> EncontrarPor(ParametrosDeQuery<T> parametrosDeQuery);
    }
}