namespace CPresentacion.Models
{
    public class LibroVM
    {
        public int IdLibro { get; set; }
        public string ImagenUrl { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
    }
}
