namespace CPresentacion.Models
{
    public class PrestamoViewModel
    {
        public int IdPrestamo { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public int IdLibro { get; set; }
        public string TituloLibro { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public string EstadoPrestamo { get; set; }
        public string Alerta { get; set; }
    }
}
