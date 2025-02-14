using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDatos
{
    public interface IDao<T>
    {
        T Obtener(int? id);
        List<T> Listar();
        void Crear(T entity);
        void Actualizar(T entity);
        void Eliminar(int id);
    }
}
