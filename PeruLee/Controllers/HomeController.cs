using System.Diagnostics;
using CDatos.Implementaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeruLee.Models;

namespace PeruLee.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibroDaoImpl _libroDao;
        private readonly ILogger<HomeController> _logger;

        public HomeController(LibroDaoImpl libroDao, ILogger<HomeController> logger)
        {
            _libroDao = libroDao;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Biblioteca()
        {
            var listaLibros = _libroDao.Listar();
            return View(listaLibros);
        }
        [HttpGet]
        public IActionResult Mision()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "2")]
        public IActionResult Prestamo()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public IActionResult Solicitar()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Libro()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Categoria()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Autor()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "1")]
        public IActionResult Usuario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}