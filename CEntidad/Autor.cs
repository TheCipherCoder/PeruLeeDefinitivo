using System;
using System.Collections.Generic;

namespace CEntidad
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        // Relación con la tabla Libro (un autor puede escribir muchos libros)
        public ICollection<Libro> Libros { get; set; }
    }
}
