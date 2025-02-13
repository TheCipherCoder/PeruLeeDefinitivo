using System.Security.Claims;
using CDatos;
using CDatos.Implementaciones;
using CPresentacion.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace PeruLee.Controllers
{
    public class AccesoController : Controller
    {
        private readonly UsuarioDaoImpl _usuarioDao;
        public AccesoController(UsuarioDaoImpl u)
        {
            _usuarioDao = u;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UsuarioVM usuariovm)
        {
            var usuario = _usuarioDao.ObtenerPorCredenciales(usuariovm.Email, usuariovm.Contrasena);

            if (usuario != null) 
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Rol.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
        }
    }
}
