using System;

namespace CEntidad
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string Caratula { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public int? IdCategoriaFk { get; set; }
        public int? IdAutorFk { get; set; }
        public int CopiasDisponibles { get; set; }
        public string Estado { get; set; }

        // Propiedades de navegación (relaciones)
        public Categoria Categoria { get; set; }
        public Autor Autor { get; set; }
    }
}
