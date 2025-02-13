using System;

namespace CEntidad
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
