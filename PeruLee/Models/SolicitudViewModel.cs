namespace CPresentacion.Models
{
    public class SolicitudViewModel
    {
        public int IdSolicitud { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int IdLibro { get; set; }
        public string TituloLibro { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public int CopiasDisponibles { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public DateTime? FechaProcesamiento { get; set; }
        public string EstadoSolicitud { get; set; }
        public int DiasEspera { get; set; }
        public int? PosicionCola { get; set; }
    }
}
