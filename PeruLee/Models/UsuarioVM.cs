using CEntidad;

namespace CPresentacion.Models
{
    public class UsuarioVM
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }

        public List<Rol> Roles { get; set; }
    }
}
